using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Olt;
using GPONMonitor.Services;
using Newtonsoft.Json;

namespace GPONMonitor.Models.Onu
{
    public abstract class OnuGeneric
    {
        public uint OltId { get; private set; }
        public uint OltPortId { get; private set; }
        public uint OltOnuId { get; private set; }
        public ModelType ModelType { get; private set; }
        public DescriptionName Description { get; private set; }
        public GponSerialNumber GponSerialNumber { get; private set; }
        public GponProfile GponProfile { get; private set; }

        public OpticalConnectionState OpticalConnectionState { get; private set; }
        public OpticalConnectionDeactivationReason OpticalConnectionDeactivationReason { get; private set; }
        public OpticalPowerReceived OpticalPowerReceived { get; private set; }
        public OpticalCableDistance OpticalCableDistance { get; private set; }

        public OpticalConnectionUptime OpticalConnectionUptime { get; private set; }
        public OpticalConnectionInactiveTime OpticalConnectionInactiveTime { get; private set; }
        public SystemUptime SystemUptime { get; private set; }

        public BlockStatus BlockStatus { get; private set; }
        public BlockReason BlockReason { get; private set; }

        [JsonIgnore]
        internal IResponseDescriptionDictionaries _responseDescriptionDictionaries;
        [JsonIgnore]
        internal IDataService _snmpDataService;


        public OnuGeneric(uint oltId, uint oltPortId, uint oltOnuId, IResponseDescriptionDictionaries responseDescriptionDictionaries, IDataService snmpDataService)
        {
            _responseDescriptionDictionaries = responseDescriptionDictionaries;
            _snmpDataService = snmpDataService;

            OltId = oltId;
            OltPortId = oltPortId;
            OltOnuId = oltOnuId;

            ModelType = new ModelType();
            Description = new DescriptionName();
            GponSerialNumber = new GponSerialNumber();
            GponProfile = new GponProfile();

            OpticalConnectionState = new OpticalConnectionState(_responseDescriptionDictionaries);
            OpticalConnectionDeactivationReason = new OpticalConnectionDeactivationReason(_responseDescriptionDictionaries);
            OpticalPowerReceived = new OpticalPowerReceived();
            OpticalCableDistance = new OpticalCableDistance();
            OpticalConnectionUptime = new OpticalConnectionUptime();
            OpticalConnectionInactiveTime = new OpticalConnectionInactiveTime();
            SystemUptime = new SystemUptime();
            BlockStatus = new BlockStatus(_responseDescriptionDictionaries);
            BlockReason = new BlockReason(_responseDescriptionDictionaries);

            ModelType.Value = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDGetOnuModelType + "." + oltPortId + "."  + oltOnuId).Result;
            Description.Value = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuDescription + "." + oltPortId + "." + oltOnuId).Result;
            GponSerialNumber.Value = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuGponSerialNumber + "." + oltPortId + "." + oltOnuId).Result;
            GponProfile.Value = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuGponProfile + "." + oltPortId + "." + oltOnuId).Result;

            OpticalConnectionState.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionState + "." + oltPortId + "." + oltOnuId).Result;
            OpticalConnectionDeactivationReason.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionDeactivationReason + "." + oltPortId + "." + oltOnuId).Result;
            OpticalPowerReceived.Value = _snmpDataService.GetStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalPowerReceived + "." + oltPortId + "." + oltOnuId).Result;
            OpticalCableDistance.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalCabelDistance + "." + oltPortId + "." + oltOnuId).Result;

            if (_snmpDataService.GetOltFirmwareVersionAsync(oltId).Result != "5.01")
            {
                // Add timers update if necessary !!!

                OpticalConnectionUptime.Value = _snmpDataService.GetIntPropertyAsync(oltId,SnmpOIDCollection.snmpOIDOnuOpticalConnectionUptime + "." + oltPortId + "." + oltOnuId).Result;
                OpticalConnectionInactiveTime.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOpticalConnectionInactiveTime + "." + oltPortId + "." + oltOnuId).Result;
                SystemUptime.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDSystemUptime + "." + oltPortId + "." + oltOnuId).Result;
            }

            BlockStatus.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockStatus + "." + oltPortId + "." + oltOnuId).Result;
            BlockReason.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockReason + "." + oltPortId + "." + oltOnuId).Result;
        }
    }
}