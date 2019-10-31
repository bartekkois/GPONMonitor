using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Models.Olt;
using GPONMonitor.Models;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using GPONMonitor.ViewModels;
using Lextm.SharpSnmpLib;
using GPONMonitor.Models.OnuFactory;
using AutoMapper;

namespace GPONMonitor.Services
{
    public class DataService : IDataService
    {
        private readonly DevicesConfiguration _devicesConfiguration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IStringLocalizer<DataService> _localizerDataService;
        private readonly IResponseDescriptionDictionaries _responseDescriptionDictionaries;
        private readonly IMapper _mapper;

        private List<Olt> configuredOlts = new List<Olt>();

        public DataService(IOptions<DevicesConfiguration> devicesConfiguration, ILoggerFactory loggerFactory, IOltFormatChecks oltFormatChecks, IResponseDescriptionDictionaries responseDescriptionDictionaries, IStringLocalizer<DataService> localizerDataService, IStringLocalizer<Olt> localizerOlt, IMapper mapper)
        {
            _devicesConfiguration = devicesConfiguration.Value;
            _loggerFactory = loggerFactory;
            _localizerDataService = localizerDataService;
            _responseDescriptionDictionaries = responseDescriptionDictionaries;
            _mapper = mapper;
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

        public IEnumerable<OltConfigurationViewModel> GetConfiguredOltListAsync()
        {
            return configuredOlts.Select(o => new OltConfigurationViewModel { Id = o.Id, Name = o.Name });
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

        public async Task<IEnumerable<OnuShortDescription>> GetOnuDescriptionListAsync(uint oltId)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetOnuDescriptionListAsync();
        }

        public async Task<string> GetStringPropertyAsync(uint oltId, string snmpOid)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetStringPropertyAsync(snmpOid);
        }

        public async Task<int?> GetIntPropertyAsync(uint oltId, string snmpOid)
        {
            return await configuredOlts.Single(s => s.Id == oltId).GetIntPropertyAsync(snmpOid);
        }

        public async Task<IOnuFactory> GetOnuStateAsync(uint oltId, uint oltPortId, uint onuId)
        {
            switch (await GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDGetOnuModelType + "." + oltPortId + "." + onuId))
            {
                case "H645B":
                    return new H645BOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H645G":
                    return new H645GOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H665":
                    return new H665OnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H665-C":
                    return new H665COnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H665G":
                    return new H665GOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H640G":
                    return new H640GOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H640GW-02":
                    return new H640GW02OnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H660GW":
                    return new H660GWOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H660GM":
                    return new H660GMOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H660RM":
                    return new H660RMOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                case "H680GW":
                    return new H680GWOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
                default:
                    return new UnknownOnuFactory(_responseDescriptionDictionaries, _mapper, this).BuildOnu(oltId, oltPortId, onuId);
            }
        }

        public async Task<IList<Variable>> SetStringPropertyAsync(uint oltId, string snmpOid, string data)
        {
            return await configuredOlts.Single(s => s.Id == oltId).SetStringPropertyAsync(snmpOid, data);
        }

        public async Task<IList<Variable>> SetIntPropertyAsync(uint oltId, string snmpOid, int data)
        {
            return await configuredOlts.Single(s => s.Id == oltId).SetIntPropertyAsync(snmpOid, data);
        }
    }
}
