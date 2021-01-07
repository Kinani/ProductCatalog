using Microsoft.AspNetCore.Http;

namespace ProductCatalog.Core.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public decimal Price { get; set; }
    }
}
