using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class H645GOnu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }

        public H645GOnu(uint oltId, uint oltPortId, uint oltOnuId, ISnmpDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, snmpDataService)
        {
            EthernetPort1State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort1State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort1Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort1Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
        }
    }
}
