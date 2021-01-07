using Microsoft.AspNetCore.Builder;

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
    }
}
