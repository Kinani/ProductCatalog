using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Core.Services;
using ProductCatalog.Infrastructure.Data;
using ProductCatalog.Infrastructure.Services;
using System;
using System.IO;
using System.Reflection;

namespace ProductCatalog.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileSystem, WebFileSystem>(x =>
                new WebFileSystem(configuration.GetValue<string>("MediaPath")));
            services.AddScoped<IFileExportService, FileExportService>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProductCatalog APIs",
                    Version = "v3",
                    Description = "A catalog for products",
                    Contact = new OpenApiContact
                    {
                        Name = "Kinani",
                        Url = new Uri("https://github.com/Kinani")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
