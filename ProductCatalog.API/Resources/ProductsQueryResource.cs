using System;

namespace ProductCatalog.API.Resources
{
    public class ProductsQueryResource : QueryResource
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal? Price { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}