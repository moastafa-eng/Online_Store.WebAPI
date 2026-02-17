using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.DbContexts;
using Persistence.Identity.Contexts;
using Persistence.Repositories;
using Persistence.UnitOfWorks;
using StackExchange.Redis;

namespace Persistence.Extensions
{
    public static class PersistenceLayerServices
    {
        public static IServiceCollection AddAllPersistenceLayerServices(this IServiceCollection services, IConfiguration config)
        {
            // Add StoreDbContex to DI container.
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")); // connection string from AppSettings.
            });

            //Add IdentityStoreDbContext to DI container
            services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("IdentityStoreConnection"));
            });

            // DI Container
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            #region Comment
            // // Use Singleton since ConnectionMultiplexer is thread-safe and should be reused
            // throughout the application's lifetime to avoid multiple Redis connections. 
            #endregion
            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(
                    config.GetConnectionString("RedisConnection"))
            );

            return services;
        }
    }
}
