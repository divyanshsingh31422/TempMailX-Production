using Serilog;
using TempMailX.Data;
using TempMailX.Services;
using TempMailX.Background;

namespace TempMailX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // MVC
            builder.Services.AddControllersWithViews();

            // Register EmailGeneratorService (MISSING THA)
            builder.Services.AddSingleton<EmailGeneratorService>();

            // Register TempEmailDAL with SQLite connection
            builder.Services.AddSingleton<TempEmailDAL>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var conn = config.GetConnectionString("Default");
                return new TempEmailDAL(conn);
            });

            //Background service (auto expiry)
            builder.Services.AddHostedService<EmailExpiryService>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            //Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Mail}/{action=Create}/{id?}");

            app.Run();
        }
    }
}
