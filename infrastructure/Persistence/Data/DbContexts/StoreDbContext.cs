using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) // Get options like connection string
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Get Configurations from Current Assembly.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        // representation the tables in DB
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
