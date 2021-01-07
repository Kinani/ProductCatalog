using ProductCatalog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(int id);
        void Update(Product product);
        void Remove(Product product);
        Task CompleteAsync();
    }
}
