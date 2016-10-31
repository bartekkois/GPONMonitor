using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class H645BOnu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }
        public EthernetPortState EthernetPort2State { get; private set; }
        public EthernetPortSpeed EthernetPort2Speed { get; private set; }

        public H645BOnu(uint oltId, uint oltPortId, uint oltOnuId, IDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, snmpDataService)
        {
            EthernetPort1State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort1State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort1Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort1Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort2State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort2State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.2").Result;
            EthernetPort2Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort2Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.2").Result;
        }
    }
}
