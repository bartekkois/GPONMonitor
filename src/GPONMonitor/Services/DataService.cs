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
using Microsoft.Extensions.Localization;

namespace GPONMonitor.Services
{
    public class DataService : IDataService
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IStringLocalizer<DataService> _localizerDataService;
        private readonly IResponseDescriptionDictionaries _responseDescriptionDictionaries;

        private List<Olt> configuredOlts = new List<Olt>();

        public DataService(IOptions<DevicesConfiguration> devicesConfiguration, ILoggerFactory loggerFactory, IOltFormatChecks oltFormatChecks, IResponseDescriptionDictionaries responseDescriptionDictionaries, IStringLocalizer<DataService> localizerDataService, IStringLocalizer<Olt> localizerOlt)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _loggerFactory = loggerFactory;
            _localizerDataService = localizerDataService;
            _responseDescriptionDictionaries = responseDescriptionDictionaries;
            var logger = _loggerFactory.CreateLogger(_localizerDataService["SNMP Data Service"]);

            try
            {
                foreach (var device in _devicesConfiguration.Devices)
                {
                    configuredOlts.Add(new Olt(device.Id, device.Name, device.IpAddress, device.SnmpPort, device.SnmpVersion, device.SnmpCommunity, device.SnmpTimeout, oltFormatChecks, localizerOlt));
                }
            }
            catch (Exception exception)
            {
                logger.LogError(_localizerDataService["enviroment configuration: "] + exception.Message);
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

        public async Task<string> GetOltFirmwareVersionAsync(uint oltId)
        {
            Regex firmwareVersionRegex = new Regex(@"([0-9]+)\.([A-Za-z0-9\-]+)");
            Match firmwareVersionMatch = firmwareVersionRegex.Match(await GetOltDescriptionAsync(oltId));

            if (firmwareVersionMatch.Success)
                return firmwareVersionMatch.Value;
            else
                throw new Exception(_localizerDataService["Error getting OLT firmware version number"]);
        }

        public async Task<List<OnuShortDescription>> GetOnuListAsync(uint oltId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetOnuDescriptionListAsync();
        }

        public async Task<string> GetStringPropertyAsync(uint oltId, string snmpOid)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetStringPropertyAsync(snmpOid);
        }

        public async Task<int> GetIntPropertyAsync(uint oltId, string snmpOid)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetIntPropertyAsync(snmpOid);
        }

        public async Task<object> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId)
        {
            switch (await GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDGetOnuModelType + "." + oltPortId + "." + onuId))
            {
                case "H645B":
                    return new H645BOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                case "H645G":
                    return new H645GOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                case "H665":
                    return new H665Onu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                case "H665G":
                    return new H665GOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                case "H640GW-02":
                    return new H665GOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                case "H660GW":
                    return new H660GWOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                case "H680GW":
                    return new H680GWOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
                default:
                    return new UnknownOnu(oltId, oltPortId, onuId, _responseDescriptionDictionaries, this);
            }
        }
    }
}
