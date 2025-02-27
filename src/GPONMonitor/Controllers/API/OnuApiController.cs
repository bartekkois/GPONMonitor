using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Olt;
using GPONMonitor.Services;
using GPONMonitor.Utils;
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

        // POST: api/ResetOnu
        [HttpPost("ResetOnu")]
        public async Task<IActionResult> ResetOnuAsync(uint oltId, uint oltPortId, uint onuId, string commandProtectionPasswordHash)
        {
            try
            {
                // Validate command protection password
                if (commandProtectionPasswordHash != HashCalculator.CalculateMD5Hash(_devicesConfiguration.Devices.Single(d => d.Id == oltId).CommandProtectionPassword))
                    return Unauthorized("Invalid command protection password.");

                // Set the SNMP properties to reset the ONU
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuResetMode, 8);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuResetOltPortId, (int)oltPortId);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuResetOnuId, (int)onuId);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuResetTimer, 0);

                return Ok("ONU reset successfully.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        // POST: api/BlockOnu
        [HttpPost("BlockOnu")]
        public async Task<IActionResult> BlockOnuAsync(uint oltId, uint oltPortId, uint onuId, string commandProtectionPasswordHash)
        {
            try
            {
                // Validate command protection password
                if (commandProtectionPasswordHash != HashCalculator.CalculateMD5Hash(_devicesConfiguration.Devices.Single(d => d.Id == oltId).CommandProtectionPassword))
                    return Unauthorized("Invalid command protection password.");

                // Set the SNMP properties to block the ONU
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockMode, 19);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockOltPortId, (int)oltPortId);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockOnuId, (int)onuId);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockType, 1);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockTimer, 0);

                return Ok("ONU successfully blocked.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        // POST: api/UnblockOnu
        [HttpPost("UnblockOnu")]
        public async Task<IActionResult> UnblockOnuAsync(uint oltId, uint oltPortId, uint onuId, string commandProtectionPasswordHash)
        {
            try
            {
                // Validate command protection password
                if (commandProtectionPasswordHash != HashCalculator.CalculateMD5Hash(_devicesConfiguration.Devices.Single(d => d.Id == oltId).CommandProtectionPassword))
                    return Unauthorized("Invalid command protection password.");

                // Set the SNMP properties to unblock the ONU
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockMode, 19);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockOltPortId, (int)oltPortId);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockOnuId, (int)onuId);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockType, 2);
                await _snmpDataService.SetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockTimer, 0);

                return Ok("ONU successfully unblocked.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}
