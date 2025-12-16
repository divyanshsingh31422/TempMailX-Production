using Serilog;

namespace TempMailX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddHostedService<TempMailX.Background.EmailExpiryService>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // ?? YE ROUTE MUST HONA CHAHIYE
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Mail}/{action=Create}/{id?}");

            app.Run();
        }
    }
}
