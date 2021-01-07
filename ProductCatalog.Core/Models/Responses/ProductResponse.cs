using ProductCatalog.Core.Entities;

namespace ProductCatalog.Core.Models.Responses
{
    public class ProductResponse : BaseResponse<Product>
    {
        public ProductResponse(Product product) : base(product) { }

        public ProductResponse(string message) : base(message) { }
    }
}