using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Models.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Infrastructure.Data
{
    // No need for base repository as project have only one domain model.
    public class ProductRepository : IProductRepository
    {
        protected readonly ProductCatalogContext _context;

        public ProductRepository(ProductCatalogContext productCatalogContext) => _context = productCatalogContext;

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<QueryResults<Product>> ListAsync(ProductsQuery query)
        {
            // Turn off tracking for EF core performace
            IQueryable<Product> queryable = _context.Products.AsNoTracking();

            // Building the where expression by ProductsQuery
            queryable = queryable.Where(p => 
                ((query.Id.HasValue && query.Id > 0) || p.Id == query.Id) &&
                (string.IsNullOrEmpty(query.Name) || query.Name.Contains(p.Name)) &&
                ((query.Price.HasValue && query.Price > 0) || query.Price == p.Price));

            // pagination count data.
            int totalItems = await queryable.CountAsync();

            // Apply pagination to Queryable
            List<Product> products = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            return new QueryResults<Product>
            {
                Items = products,
                TotalItems = totalItems,
            };
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        // No need to have implementation for UnitOfWork as we only have one domain model.
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
