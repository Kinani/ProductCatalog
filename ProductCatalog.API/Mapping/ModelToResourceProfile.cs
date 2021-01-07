using AutoMapper;
using ProductCatalog.API.Resources;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Models.Queries;

namespace ProductCatalog.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<QueryResults<Product>, QueryResultsResource<ProductResource>>();
        }
    }
}