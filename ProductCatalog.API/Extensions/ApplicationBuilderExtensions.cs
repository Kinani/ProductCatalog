using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCatalog APIs");
                options.DocumentTitle = "ProductCatalog APIs";
            });
            return app;
        }

        public static IApplicationBuilder ApplyEfMigrations(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductCatalogContext>();
            if (dbContext != null)
                dbContext.Database.Migrate();

            return app;
        }
    }
}
