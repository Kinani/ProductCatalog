namespace ProductCatalog.Core.Models
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        // Update
        public string Photo { get; set; }
        public decimal? Price { get; set; }
    }
}
