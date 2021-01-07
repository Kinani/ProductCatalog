using System.Collections.Generic;

namespace ProductCatalog.Core.Models.Queries
{
    public class QueryResults<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
    }
}
