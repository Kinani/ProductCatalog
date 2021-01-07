using System;

namespace ProductCatalog.Core.Models.Queries
{
    public class ProductsQuery : Query
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public DateTime? LastUpdated { get; set; }

        public ProductsQuery(int? id, 
            string name, 
            decimal? price, 
            DateTime? lastUpdated, 
            int? page, 
            int? itemsPerPage) : base(page, itemsPerPage)
        {
            Id = id;
            Name = name;
            Price = price;
            LastUpdated = lastUpdated;
        }
    }
}
