using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Olt;
using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class H665GOnu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }

        public H665GOnu(uint oltId, uint oltPortId, uint oltOnuId, IResponseDescriptionDictionaries responseDescriptionDictionaries, IDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, responseDescriptionDictionaries, snmpDataService)
        {
            EthernetPort1State = new EthernetPortState(_responseDescriptionDictionaries);
            EthernetPort1Speed = new EthernetPortSpeed(_responseDescriptionDictionaries);

            EthernetPort1State.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort1Speed.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
        }
    }
}
