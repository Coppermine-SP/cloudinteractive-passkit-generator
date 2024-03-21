using Cloudinteractive.PassKitGenerator.Services;
using Cloudinteractive.PassKitGenerator.Services.Template;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Cloudinteractive.passkit_generator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            try
            {
                Logging.LoggerFactory = app.Services.GetService<ILoggerFactory>();
                TemplateManager.Init();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Init failed: " + e);
                Console.WriteLine("\n\nApplication aborted.");
                return;
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}