using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Extensions;
using Persistence.Identity.Contexts;
using Services.Extensions;
using Shard.JWT;
using Shard.ModelErrors;

namespace Online_Store.Web.Extensions
{
    public static class AddAllServices
    {
        public static IServiceCollection AddAllApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddWebServices();

            services.AddIdentityServices();

            services.AddAllPersistenceLayerServices(config);

            services.AddServiceLayerServices(config);

            services.ConfigureApiBehaviorOptions();

            // Get Options from AppSetting in JwtOptions section
            services.Configure<JwtOptions>(config.GetSection("JwtOptions"));
            return services;
        }

        private static  IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                // Identity Configurations
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>() // Add RoleManager
            .AddEntityFrameworkStores<IdentityStoreDbContext>(); // AddIdentityStoreDbContext

            return services;
        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            // Replace the default BadRequest response with a custom response when there are errors in ModelState
            services.Configure<ApiBehaviorOptions>(config => // ApiBehaviorOptions configures the behavior of how the ModelState is handled in the API

            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    // Check if there are any errors in the ModelState
                    var errores = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                    .Select(M => new ValidationError
                    {
                        Field = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage) // Map the error messages from the ModelState into the Errors field of ValidationError

                    }).ToList();

                    // Mapping all errors from ModelSate to ValidationErrrorResponse object
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errores
                    };

                    // return BadRequest With Response (write response with Json formate)
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
