using camera_trigger_api_core.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace camera_trigger_api_core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConnectionString"];
            services.AddTransient(typeof(ITriggerContext), typeof(TriggerContext));
            services.AddDbContext<TriggerContext>(opt =>
            opt.UseSqlServer(connection));
            services.AddCors();
            services.AddMvc()

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json",
                     optional: false,
                     reloadOnChange: true)
                     .AddEnvironmentVariables()
        .AddEnvironmentVariables("APPSETTING_");

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
