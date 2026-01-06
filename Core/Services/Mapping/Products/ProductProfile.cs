using AutoMapper;
using Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using Shard.DTOs.Products;

namespace Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration config)
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(des => des.Brand, opt => opt.MapFrom(source => source.Brand.Name))
                .ForMember(des => des.Type, opt => opt.MapFrom(source => source.Type.Name))
                //.ForMember(des => des.PictureUrl, opt => opt.MapFrom(source => $"{config["BaseUrl"]}/{source.PictureUrl}"));
            #region IValueResolver
                //**`IValueResolver`** is used when you need to apply complex logic or calculations to
                //a specific value in the **source** before mapping it to the **destination**.
            #endregion
                .ForMember(des => des.PictureUrl, opt => opt.MapFrom(new ProductPictureUrlResolver(config)));
            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();

            // https://localhost:7177
        }
    }
}
