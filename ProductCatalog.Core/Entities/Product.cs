using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Core.Entities
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
