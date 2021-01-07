using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProductCatalog.Infrastructure.Data
{
    public class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
