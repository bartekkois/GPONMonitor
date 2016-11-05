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
        public Description Description { get; private set; }
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
        internal IDataService _snmpDataService;

        public OnuGeneric(uint oltId, uint oltPortId, uint oltOnuId, IDataService snmpDataService)
        {
            _snmpDataService = snmpDataService;

            OltId = oltId;
            OltPortId = oltPortId;
            OltOnuId = oltOnuId;

            ModelType = new ModelType();
            Description = new Description();
            GponSerialNumber = new GponSerialNumber();
            GponProfile = new GponProfile();

            OpticalConnectionState = new OpticalConnectionState();
            OpticalConnectionDeactivationReason = new OpticalConnectionDeactivationReason();
            OpticalPowerReceived = new OpticalPowerReceived();
            OpticalCableDistance = new OpticalCableDistance();
            OpticalConnectionUptime = new OpticalConnectionUptime();
            OpticalConnectionInactiveTime = new OpticalConnectionInactiveTime();
            SystemUptime = new SystemUptime();
            BlockStatus = new BlockStatus();
            BlockReason = new BlockReason();

            ModelType.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDGetOnuModelType + "." + oltPortId + "."  + oltOnuId).Result;
            Description.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuDescription + "." + oltPortId + "." + oltOnuId).Result;
            GponSerialNumber.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuGponSerialNumber + "." + oltPortId + "." + oltOnuId).Result;
            GponProfile.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuGponProfile + "." + oltPortId + "." + oltOnuId).Result;

            OpticalConnectionState.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionState + "." + oltPortId + "." + oltOnuId).Result;
            OpticalConnectionDeactivationReason.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalConnectionDeactivationReason + "." + oltPortId + "." + oltOnuId).Result;
            OpticalPowerReceived.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalPowerReceived + "." + oltPortId + "." + oltOnuId).Result;
            OpticalCableDistance.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuOpticalCabelDistance + "." + oltPortId + "." + oltOnuId).Result;

            if (_snmpDataService.GetOltFirmwareVersionAsync(oltId).Result != "5.01")
            {
                // Add timers update if necessary !!!

                OpticalConnectionUptime.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId,SnmpOIDCollection.snmpOIDOnuOpticalConnectionUptime + "." + oltPortId + "." + oltOnuId).Result;
                OpticalConnectionInactiveTime.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOpticalConnectionInactiveTime + "." + oltPortId + "." + oltOnuId).Result;
                SystemUptime.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDSystemUptime + "." + oltPortId + "." + oltOnuId).Result;
            }

            BlockStatus.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockStatus + "." + oltPortId + "." + oltOnuId).Result;
            BlockReason.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuBlockReason + "." + oltPortId + "." + oltOnuId).Result;
        }
    }
}