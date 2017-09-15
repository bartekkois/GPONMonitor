using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Models.Olt;
using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class H640GOnu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }
        public EthernetPortState EthernetPort2State { get; private set; }
        public EthernetPortSpeed EthernetPort2Speed { get; private set; }
        public EthernetPortState EthernetPort3State { get; private set; }
        public EthernetPortSpeed EthernetPort3Speed { get; private set; }
        public EthernetPortState EthernetPort4State { get; private set; }
        public EthernetPortSpeed EthernetPort4Speed { get; private set; }

        public H640GOnu(uint oltId, uint oltPortId, uint oltOnuId, IResponseDescriptionDictionaries responseDescriptionDictionaries, IDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, responseDescriptionDictionaries, snmpDataService)
        {
            EthernetPort1State = new EthernetPortState(_responseDescriptionDictionaries);
            EthernetPort1Speed = new EthernetPortSpeed(_responseDescriptionDictionaries);
            EthernetPort2State = new EthernetPortState(_responseDescriptionDictionaries);
            EthernetPort2Speed = new EthernetPortSpeed(_responseDescriptionDictionaries);
            EthernetPort3State = new EthernetPortState(_responseDescriptionDictionaries);
            EthernetPort3Speed = new EthernetPortSpeed(_responseDescriptionDictionaries);
            EthernetPort4State = new EthernetPortState(_responseDescriptionDictionaries);
            EthernetPort4Speed = new EthernetPortSpeed(_responseDescriptionDictionaries);

            EthernetPort1State.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort1Speed.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort2State.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + oltOnuId + ".1.2").Result;
            EthernetPort2Speed.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + oltOnuId + ".1.2").Result;
            EthernetPort3State.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + oltOnuId + ".1.3").Result;
            EthernetPort3Speed.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + oltOnuId + ".1.3").Result;
            EthernetPort4State.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortState + "." + oltPortId + "." + oltOnuId + ".1.4").Result;
            EthernetPort4Speed.Value = _snmpDataService.GetIntPropertyAsync(oltId, SnmpOIDCollection.snmpOIDOnuEthernetPortSpeed + "." + oltPortId + "." + oltOnuId + ".1.4").Result;
        }
    }
}
