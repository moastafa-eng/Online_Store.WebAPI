using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.Mapping.Products;

namespace Services.Extensions
{
    public static class ServicesLayerServices
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(config)));

            return services;
        }
    }
}
