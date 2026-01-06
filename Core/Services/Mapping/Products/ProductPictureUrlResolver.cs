using AutoMapper;
using Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using Shard.DTOs.Products;

namespace Services.Mapping.Products
{
    public class ProductPictureUrlResolver(IConfiguration config) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            // if image is null or empty return BaseURL + ImageURL else return empty string
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{config["BaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
