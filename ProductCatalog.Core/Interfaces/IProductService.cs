using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Models.Queries;
using ProductCatalog.Core.Models.Responses;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Interfaces
{
    public interface IProductService
    {
        Task<QueryResults<Product>> ListAsync(ProductsQuery query);
        Task<ProductResponse> SaveAsync(ProductDto productDto);
        Task<ProductResponse> UpdateAsync(int id, ProductDto productDto);
        Task<ProductResponse> DeleteAsync(int id);
        Task<ExportResponse> ExportExcel(ProductsQuery productsQuery);
    }
}
