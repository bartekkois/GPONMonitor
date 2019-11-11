using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace GPONMonitor.Controllers
{
    public class OltController : Controller
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private IDataService _dataService;
        private readonly IStringLocalizer<OltController> _localizer;

        public OltController(IOptions<DevicesConfiguration> devicesConfiguration, IDataService dataService, IStringLocalizer<OltController> localizer)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _dataService = dataService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Version"] = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return View(_dataService.GetConfiguredOltListAsync());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
