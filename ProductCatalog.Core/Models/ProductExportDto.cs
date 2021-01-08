using System;
using System.ComponentModel;

namespace ProductCatalog.Core.Models
{
    public class ProductExportDto
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Photo path")]
        public string Photo { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Last updated")]
        public string LastUpdated { get; set; }
    }
}
