
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.DbContexts;
using System.Threading.Tasks;

namespace Online_Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add StoreDbContex to DI container.
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // connection string from AppSettings.
            });

            // DI Container
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            var app = builder.Build();

            #region DBInitializer
            // Create scop and ask CLR to create obj from IDbIntializer
            var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync(); 
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
