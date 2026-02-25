using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.Mapping.Auth;
using Services.Mapping.Baskets;
using Services.Mapping.Orders;
using Services.Mapping.Products;

namespace Services.Extensions
{
    public static class ServicesLayerServices
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(config)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            services.AddAutoMapper(m => m.AddProfile(new OrderProfile()));
            services.AddAutoMapper(m => m.AddProfile(new AuthProfile()));

            return services;
        }
    }
}
