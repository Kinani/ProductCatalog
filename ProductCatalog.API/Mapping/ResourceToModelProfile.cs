using AutoMapper;
using ProductCatalog.API.Resources;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Models.Queries;

namespace ProductCatalog.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveProductResource, ProductDto>();

            CreateMap<ProductsQueryResource, ProductsQuery>();
        }
    }
}