using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Microsoft.Extensions.Options;
using System;

namespace GPONMonitor.Controllers.API
{
    [Route("api/Olt")]
    public class OltApiController : Controller
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private IDataService _snmpDataService;

        public OltApiController(IOptions<DevicesConfiguration> devicesConfiguration, IDataService snmpDataService)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _snmpDataService = snmpDataService;
        }

        // GET: api/Onu/?oltId=1
        [HttpGet]
        public async Task<IActionResult> GetOnuDescriptionListAsync(uint oltId)
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
