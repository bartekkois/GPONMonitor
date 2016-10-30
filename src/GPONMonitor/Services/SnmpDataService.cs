using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Olt;
using GPONMonitor.Models;
using GPONMonitor.Models.Onu;
using System.Text.RegularExpressions;

namespace GPONMonitor.Services
{
    public class SnmpDataService : ISnmpDataService
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private readonly ILoggerFactory _loggerFactory;
        private List<Olt> configuredOlts = new List<Olt>();

        public SnmpDataService(IOptions<DevicesConfiguration> devicesConfiguration, ILoggerFactory loggerFactory)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _loggerFactory = loggerFactory;
            var logger = _loggerFactory.CreateLogger("SNMP Data Service");

            try
            {
                foreach (var device in _devicesConfiguration.Devices)
                {
                    configuredOlts.Add(new Olt(device.Id, device.Name, device.IpAddress, device.SnmpPort, device.SnmpVersion, device.SnmpCommunity, device.SnmpTimeout));
                }
            }
            catch (Exception exception)
            {
                logger.LogError("enviroment configuration: " + exception.Message);
            }
        }

        public async Task<string> GetOltDescriptionAsync(uint oltId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetDescriptionAsync();
        }

        public async Task<string> GetOltUptimeAsync(uint oltId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetUptimeAsync();
        }

        public string GetOltFirmwareVersionAsync(uint oltId)
        {
            Regex firmwareVersionRegex = new Regex(@"([0-9]+)\.([A-Za-z0-9\-]+)");
            Match firmwareVersionMatch = firmwareVersionRegex.Match(GetOltDescriptionAsync(oltId).Result);

            if (firmwareVersionMatch.Success)
                return firmwareVersionMatch.Value;
            else
                throw new Exception("Error parisng OLT firmware version number");
        }

        public async Task<List<OnuShortDescription>> GetOnuListAsync(uint oltId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetOnuDescriptionListAsync();
        }

        public async Task<string> GetOnuModelAsync(uint oltId, uint oltPortId, uint onuId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetOnuModelAsync(oltPortId, onuId);
        }

        public async Task<object> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId)
        {
            switch (await GetOnuModelAsync(oltId, oltPortId, onuId))
            {
                case "H645B":
                    return new H645GOnu(oltId, oltPortId, onuId);
                case "H645G":
                    return new H645GOnu(oltId, oltPortId, onuId);
                case "H665G":
                    return new H665GOnu(oltId, oltPortId, onuId);
                case "H640GW-02":
                    return new H665GOnu(oltId, oltPortId, onuId);
                default:
                    return new UnknownOnu(oltId, oltPortId, onuId);
            }
        }
    }
}
