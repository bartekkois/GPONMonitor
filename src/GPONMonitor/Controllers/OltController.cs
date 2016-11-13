using Microsoft.AspNetCore.Mvc;
using GPONMonitor.Models.Configuration;
using Microsoft.Extensions.Options;
using GPONMonitor.Services;
using Microsoft.Extensions.Localization;

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
            return View(_dataService.GetConfiguredOltListAsync());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
