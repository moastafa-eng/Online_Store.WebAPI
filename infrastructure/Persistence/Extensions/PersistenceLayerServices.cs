using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.DbContexts;
using Persistence.UnitOfWorks;

namespace Persistence.Extensions
{
    public static class PersistenceLayerServices
    {
        public static IServiceCollection AddAllPersistenceLayerServices(this IServiceCollection services, IConfiguration confing)
        {
            // Add StoreDbContex to DI container.
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(confing.GetConnectionString("DefaultConnection")); // connection string from AppSettings.
            });

            // DI Container
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
