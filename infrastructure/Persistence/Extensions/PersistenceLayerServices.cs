using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.DbContexts;
using Persistence.UnitOfWorks;
using StackExchange.Redis;

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

            #region Comment
            // // Use Singleton since ConnectionMultiplexer is thread-safe and should be reused
            // throughout the application's lifetime to avoid multiple Redis connections. 
            #endregion
            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(
                    confing.GetConnectionString("RedisConnection"))
            );

            return services;
        }
    }
}
