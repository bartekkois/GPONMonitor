using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace GPONMonitor.Controllers.API
{
    [Route("api")]
    public class OltApiController : Controller
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private IDataService _snmpDataService;

        public OltApiController(IOptions<DevicesConfiguration> devicesConfiguration, IDataService snmpDataService)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _snmpDataService = snmpDataService;
        }

        // GET: api/OltOnuDescriptionList?oltId=1
        [HttpGet("OltOnuDescriptionList")]
        public async Task<IActionResult> GetOltOnuDescriptionListAsync(uint oltId)
        {
            try
            {
                return Json(await _snmpDataService.GetOnuDescriptionListAsync(oltId));
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
