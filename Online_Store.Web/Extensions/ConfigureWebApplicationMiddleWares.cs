using Domain.Contracts;
using Online_Store.Web.MiddleWares;

namespace Online_Store.Web.Extensions
{
    public static class ConfigureWebApplicationMiddleWares
    {
        public static async Task<WebApplication> ConfigureMiddleWares(this WebApplication app)
        {
            await app.DataSeed();

            app.UseGlobalErrorHandling();

            app.UseStaticFiles(); // Enable static files during response

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> DataSeed(this WebApplication app)
        {
            // Create scop and ask CLR to create obj from IDbIntializer
            var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();

            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

            return app;
        }
    }
}
