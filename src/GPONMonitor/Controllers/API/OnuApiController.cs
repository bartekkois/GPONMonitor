using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GPONMonitor.Controllers
{
    [Route("api")]
    public class OnuApiController : Controller
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private IDataService _snmpDataService;

        public OnuApiController(IOptions<DevicesConfiguration> devicesConfiguration, IDataService snmpDataService)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _snmpDataService = snmpDataService;
        }

        // GET: api/OnuStateByOltPortIdAndOnuId?oltId=1&oltPortId=2&onuId=3
        [HttpGet("OnuStateByOltPortIdAndOnuId")]
        public async Task<IActionResult> GetOnuStateByOltPortIdAndOnuIdAsync(uint oltId, uint oltPortId, uint onuId)
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

        // GET: api/OnuStateByOnuSerialNumber?oltId=0&onuSerialNumber=DSNWcbd38907
        [HttpGet("OnuStateByOnuSerialNumber")]
        public async Task<IActionResult> GetOnuStateByOnuSerialNumberAsync(uint oltId, string onuSerialNumber)
        {
            try
            {
                var onu = (await _snmpDataService.GetOnuDescriptionListAsync(oltId)).SingleOrDefault(s => s.OnuGponSerialNumber == onuSerialNumber);

                if (onu == null)
                    return NotFound();

                return Json(await _snmpDataService.GetOnuStateAsync(oltId, onu.OltPortId, onu.OnuId));
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
