using System.Collections.Generic;

namespace ProductCatalog.API.Resources
{
    public class QueryResultsResource<T>
    {
        public int TotalItems { get; set; } = 0;
        public List<T> Items { get; set; } = new List<T>();
    }
}