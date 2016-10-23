using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GPONMonitor.Models.Configuration;
using Microsoft.Extensions.Options;
using GPONMonitor.Services;
using System;

namespace GPONMonitor.Controllers
{
    public class OltController : Controller
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private ISnmpDataService _snmpDataService;

        public OltController(IOptions<DevicesConfiguration> devicesConfiguration, ISnmpDataService snmpDataService)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _snmpDataService = snmpDataService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var description = await _snmpDataService.GetOltDescriptionAsync(2);
                var uptime = await _snmpDataService.GetOltUptimeAsync(2);
                var onulist = await _snmpDataService.GetOnuListAsync(2);
                var onudetail = await _snmpDataService.GetOnuDetailInfoAsync(2, 1, 2);

                ViewData["Message"] = description + uptime;
                ViewData["onulist"] = onulist;
                ViewData["onudetail"] = onudetail;
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
