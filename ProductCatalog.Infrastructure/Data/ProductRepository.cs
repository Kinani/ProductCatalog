using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Interfaces;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Product>> ListAsync()
        {
            // As no tracking to increase performace of EF core
            return await _context.Products
                                 .AsNoTracking()
                                 .ToListAsync();
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
