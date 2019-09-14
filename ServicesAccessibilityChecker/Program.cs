using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ServicesAccessibilityChecker.Context;

namespace ServicesAccessibilityChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ServicesDbContext())
            {
                context.Database.Migrate();
            }
                CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
                //.ConfigureLogging(logging =>
                //{
                //    logging.ClearProviders();
                //    logging.SetMinimumLevel(LogLevel.Trace);
                //})
                //.UseNLog();
    }
}
