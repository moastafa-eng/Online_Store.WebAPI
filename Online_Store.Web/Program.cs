using Online_Store.Web.Extensions;


namespace Online_Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAllApplicationServices(builder.Configuration);



            var app = builder.Build();

            await app.ConfigureMiddleWares();

            app.Run();
        }
    }
}
