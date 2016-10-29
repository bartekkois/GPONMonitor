using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GPONMonitor.Models.Onu;
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
        private ISnmpDataService _snmpDataService;

        public OnuApiController(IOptions<DevicesConfiguration> devicesConfiguration, ISnmpDataService snmpDataService)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _snmpDataService = snmpDataService;
        }

        // GET: api/Onu/?oltId=1&oltPortId=2&onuId=3
        [HttpGet]
        public async Task<IActionResult> Get(uint oltId, uint oltPortId, uint onuId)
        {
            string modelName = "";

            try
            {
                modelName = await _snmpDataService.GetOnuModelAsync(oltId, oltPortId, onuId);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }

            var onuDetailInfo = new object();

            switch (modelName)
            {
                case "H645B":
                    onuDetailInfo = new H645BOnu(oltPortId, onuId);
                    break;
                case "H645G":
                    onuDetailInfo = new H645GOnu(oltPortId, onuId);
                    break;
                case "H665G":
                    onuDetailInfo = new H665GOnu(oltPortId, onuId);
                    break;
                case "H640GW-02":
                    onuDetailInfo = new H665GOnu(oltPortId, onuId);
                    break;
                default:
                    onuDetailInfo = new UnknownOnu(oltPortId, onuId);
                    break;
            }

            return Json(onuDetailInfo);
        }
    }
}
