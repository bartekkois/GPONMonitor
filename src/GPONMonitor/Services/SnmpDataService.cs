using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Olt;
using GPONMonitor.Models;

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

        public async Task<List<OnuShortDescription>> GetOnuListAsync(uint oltId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetOnuDescriptionListAsync();
        }

        public async Task<string> GetOnuModelAsync(uint oltId, uint oltPortId, uint onuId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetOnuModelAsync(oltPortId, onuId);
        }
    }
}
