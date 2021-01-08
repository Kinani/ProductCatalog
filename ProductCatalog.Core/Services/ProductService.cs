using Microsoft.Extensions.Caching.Memory;
using ProductCatalog.Core.Cache;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Models.Queries;
using ProductCatalog.Core.Models.Responses;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileSystem _fileSystem;
        private readonly IFileExportService _fileExportService;
        private readonly IMemoryCache _cache;

        public ProductService(IProductRepository productRepository,
            IFileSystem fileSystem,
            IFileExportService fileExportService,
            IMemoryCache cache)
        {
            _productRepository = productRepository;
            _fileSystem = fileSystem;
            _fileExportService = fileExportService;
            _cache = cache;
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found.");

            try
            {
                _fileSystem.DeletePicture(existingProduct.Photo);
                _productRepository.Remove(existingProduct);
                await _productRepository.CompleteAsync();
                // Remove from memory cache
                _cache.Remove(GetCacheKeyForProductQuery(id));

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }

        public async Task<QueryResults<Product>> ListAsync(ProductsQuery query)
        {
            return await _productRepository.ListAsync(query);
        }

        public async Task<ProductResponse> SaveAsync(ProductDto productDto)
        {
            try
            {
                var product = new Product()
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    LastUpdated = DateTime.Now,
                    Photo = await _fileSystem.SavePicture(productDto.Photo)
                };

                await _productRepository.AddAsync(product);
                await _productRepository.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }

        }

        public async Task<ProductResponse> UpdateAsync(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found.");
            try
            {
                existingProduct.Name = productDto.Name;
                existingProduct.LastUpdated = DateTime.Now;
                existingProduct.Price = productDto.Price;
                existingProduct.Photo = await _fileSystem.ReplacePicture(productDto.Photo, existingProduct.Photo);


                _productRepository.Update(existingProduct);
                await _productRepository.CompleteAsync();
                _cache.Remove(GetCacheKeyForProductQuery(id));

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> GetAsync(int id)
        {
            try
            {
                var cacheKey = GetCacheKeyForProductQuery(id);

                var product = await _cache.GetOrCreateAsync(cacheKey, (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return _productRepository.FindByIdAsync(id);
                });

                if (product == null)
                    return new ProductResponse("No product with this Id existing");

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when retrieving the product: {ex.Message}");
            }

        }

        public async Task<ExportResponse> ExportExcel(ProductsQuery productsQuery)
        {
            try
            {
                var products = await ListAsync(productsQuery);

                if (products.Items == null || products.Items.Count == 0)
                    return new ExportResponse("No products found");

                var memoryStream = _fileExportService.ExportExcel(products.Items.Select(p =>
                new ProductExportDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Photo = p.Photo,
                    Price = p.Price,
                    LastUpdated = p.LastUpdated.ToString("dd/MM/yyyy HH:mm:ss")
                }), "Products");

                return new ExportResponse(memoryStream);
            }
            catch (Exception ex)
            {
                return new ExportResponse($"An error occurred when exporting products: {ex.Message}");
            }
        }

        // Cache single product
        private string GetCacheKeyForProductQuery(int id)
        {
            string key = CacheKeys.Product.ToString();

            key = string.Concat(key, "_", id);
            return key;
        }
    }
}
