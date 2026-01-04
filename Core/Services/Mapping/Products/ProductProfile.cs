using AutoMapper;
using Domain.Entities.Products;
using Shard.DTOs.Products;

namespace Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(p => p.Brand, opt => opt.MapFrom(b => b.Brand.Name))
                .ForMember(p => p.Type, opt => opt.MapFrom(t => t.Type.Name));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
