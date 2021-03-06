﻿namespace ProductCatalog.Core.Models.Queries
{
    public class Query
    {
        public int Page { get; protected set; }
        public int ItemsPerPage { get; protected set; }

        public Query(int? page, int? itemsPerPage)
        {
            Page = page.GetValueOrDefault();
            ItemsPerPage = itemsPerPage.GetValueOrDefault();

            if (Page <= 0)
            {
                Page = 1;
            }

            if (ItemsPerPage <= 0)
            {
                ItemsPerPage = 10;
            }
        }
    }
}