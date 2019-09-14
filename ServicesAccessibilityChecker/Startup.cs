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
            services.AddSingleton<CheckStatusJob>();
            services.AddSingleton<IFullInfo, FullInfo>();
            services.AddSingleton<IStatusChecker, StatusChecker>();
            services.AddSingleton<Repository>();
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
            app.UseHttpsRedirection();//todo можно было бы добавить lodding middleware, но не хватило времени
            app.UseMvc();
        }
    }
}
