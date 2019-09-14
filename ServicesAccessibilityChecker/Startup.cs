using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesAccessibilityChecker.Extensions;
using ServicesAccessibilityChecker.Models;
using ServicesAccessibilityChecker.Scheduling;

namespace ServicesAccessibilityChecker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CheckStatusJob>();
            services.AddScoped<FullInfo>();
            services.AddScoped<Repository>();
            services.AddScoped<StatusChecker>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.ConfigureCors();

            CheckScheduler.Start();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();//addloggingmiddleware
            app.UseMvc();
        }
    }
}
