using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Models.Queries;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<QueryResults<Product>> ListAsync(ProductsQuery query);
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(int id);
        void Update(Product product);
        void Remove(Product product);
        Task CompleteAsync();
    }
}
