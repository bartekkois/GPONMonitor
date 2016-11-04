using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Microsoft.Extensions.Options;
using System;

namespace GPONMonitor.Controllers
{
    [Route("api/Onu")]
    public class OnuApiController : Controller
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private IDataService _snmpDataService;

        public OnuApiController(IOptions<DevicesConfiguration> devicesConfiguration, IDataService snmpDataService)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _snmpDataService = snmpDataService;
        }

        // GET: api/Onu/?oltId=1&oltPortId=2&onuId=3
        [HttpGet]
        public async Task<IActionResult> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId)
        {
            try
            {
                return Json(await _snmpDataService.GetOnuStateAsync(oltId, oltPortId, onuId));
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
