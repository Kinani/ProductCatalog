using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.API.Resources
{
    public class SaveProductResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}