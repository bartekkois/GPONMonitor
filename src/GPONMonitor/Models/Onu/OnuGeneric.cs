using GPONMonitor.Models.ComplexStateTypes;
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

        public OpticalConnectionState OpticalConnectionState { get; private set; }
        public OpticalConnectionDeactivationReason OpticalConnectionDeactivationReason { get; private set; }
        public OpticalPowerReceived OpticalPowerReceived { get; private set; }
        public OpticalCableDistance OpticalCableDistance { get; private set; }

        public OpticalConnectionUptime OpticalConnectionUptime { get; private set; }
        public OpticalConnectionInactiveTime OpticalConnectionInactiveTime{ get; private set; }
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

            ModelType.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, ModelType.SnmpOID + "." + oltPortId + "."  + oltOnuId).Result;
            Description.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, Description.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            GponSerialNumber.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, GponSerialNumber.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;

            OpticalConnectionState.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, OpticalConnectionState.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            OpticalConnectionDeactivationReason.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, OpticalConnectionDeactivationReason.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            OpticalPowerReceived.Value = _snmpDataService.GetOnuStringPropertyAsync(oltId, OpticalPowerReceived.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            OpticalCableDistance.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, OpticalCableDistance.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;

            OpticalConnectionUptime.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, OpticalConnectionUptime.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            OpticalConnectionInactiveTime.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, OpticalConnectionInactiveTime.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            SystemUptime.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, SystemUptime.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;

            BlockStatus.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, BlockStatus.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
            BlockReason.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, BlockReason.SnmpOID + "." + oltPortId + "." + oltOnuId).Result;
        }
    }
}