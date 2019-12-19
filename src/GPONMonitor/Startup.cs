using AutoMapper;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Olt;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Linq;

namespace GPONMonitor
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("devicesconfiguration.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddOptions();
            services.Configure<DevicesConfiguration>(Configuration.GetSection("DevicesConfiguration"));
            services.Configure<DevicesConfiguration>(devicesConfigurationOptions =>
            {
                foreach (var device in devicesConfigurationOptions.Devices.Select((value, index) => new { value, index }))
                {
                    device.value.Id = device.index;
                }
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IDataService, DataService>();
            services.AddSingleton<IOltFormatChecks, OltFormatChecks>();
            services.AddSingleton<IResponseDescriptionDictionaries, ResponseDescriptionDictionaries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Olt/Error");
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Olt}/{action=Index}/{id?}");
            });
        }
    }
}
