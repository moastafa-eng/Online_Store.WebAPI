// Ignore Spelling: Initializer

using Domain.Contracts;
using Domain.Entities.Identity;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using Persistence.Identity.Contexts;
using System.Text.Json;

namespace Persistence
{
    public class DbInitializer(
        StoreDbContext _context,
        IdentityStoreDbContext _identityDbContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            //Asynchronously get all migrations that are defined in assembly but have not been applied to the target data base. 
            var pendingMigurations = await _context.Database.GetPendingMigrationsAsync();


            // Create DB if it does not exist and applying any pending migrations. 
            if (pendingMigurations.Any())
            {
                await _context.Database.MigrateAsync();
            }


            //------------------------
            // Data Seeding
            //------------------------

            // check is there is any data in ProductBrands table
            if(!_context.ProductBrands.Any())
            {

                // ProductBrand Seeding 

                // Read all text from Brands.Json
                var productBrandsData = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Data Seeding\brands.json");

                // convert Json text to List of ProductBrands
                var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);

                // add data to productBrands table if it not null and it's cont > 0
                if(productBrands is not null && productBrands.Count > 0)
                {
                    await _context.AddRangeAsync(productBrands);
                }
            }

            // Check is there any data in ProductTypes table
            if(!_context.ProductTypes.Any())
            {
                // Read all text from type.Json
                var prodcutTypesData = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Data Seeding\types.json");

                // Convert Json text to list of ProductType
                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(prodcutTypesData);

                // Add data to ProductTypes table if it not null and it's count > 0
                if (productTypes is not null && productTypes.Count > 0)
                {
                    await _context.AddRangeAsync(productTypes);
                }
            }

            // Check is there any data in Products table
            if(!_context.Products.Any())
            {
                // Read all text from products.Json
                var productData = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Data Seeding\products.json");

                // convert Json text to list  of products
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                // add data to products table if it not null and it's count > 0
                if (products is not null && products.Count() > 0)
                {
                    await _context.AddRangeAsync(products);
                }
            }

            if(!_context.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllTextAsync(@"..\infrastructure\Persistence\Data\Data Seeding\delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                    await _context.AddRangeAsync(deliveryMethods);
            }

            // Save all changes
            await _context.SaveChangesAsync();
        }

        public async Task InitializeIdentityAsync()
        {
            var pendingMigrations = await _identityDbContext.Database.GetPendingMigrationsAsync();

            // MigrateAsync : Update database or Create if it not found
            if(pendingMigrations.Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            // Data seeding

            if(!_identityDbContext.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            if(!_identityDbContext.Users.Any())
            {
                var superAdmin = new AppUser
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperName@gmail.com",
                    PhoneNumber = "01155301076"
                };

                var admin = new AppUser
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01102089567"
                };

                await _userManager.CreateAsync(superAdmin, "Kb@103507");
                await _userManager.CreateAsync(admin, "Kb@103507");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

            await _identityDbContext.SaveChangesAsync();
        }
    }
}
