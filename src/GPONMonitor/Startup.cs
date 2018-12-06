using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Olt;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Linq;
using AutoMapper;

namespace GPONMonitor
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("devicesconfiguration.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddOptions();
            services.Configure<DevicesConfiguration>(Configuration.GetSection("DevicesConfiguration"));

            services.Configure<DevicesConfiguration>(devicesConfigurationOptions =>
            {
                foreach (var device in devicesConfigurationOptions.Devices.Select((value, index) => new { value, index }))
                {
                    device.value.Id = device.index;
                }
            });

            services.AddAutoMapper();
            services.AddScoped<IDataService, DataService>();
            services.AddSingleton<IOltFormatChecks, OltFormatChecks>();
            services.AddSingleton<IResponseDescriptionDictionaries, ResponseDescriptionDictionaries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[]
            {
                      new CultureInfo("en-US"),
                      new CultureInfo("en-GB"),
                      new CultureInfo("en"),
                      new CultureInfo("pl-PL"),
                      new CultureInfo("pl")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"), SupportedCultures = supportedCultures, SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Olt/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Olt}/{action=Index}/{id?}");
            });
        }
    }
}
