using GPONMonitor.Models.ComplexStateTypes;
using GPONMonitor.Services;

namespace GPONMonitor.Models.Onu
{
    public class H640GW02Onu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }
        public EthernetPortState EthernetPort2State { get; private set; }
        public EthernetPortSpeed EthernetPort2Speed { get; private set; }
        public EthernetPortState EthernetPort3State { get; private set; }
        public EthernetPortSpeed EthernetPort3Speed { get; private set; }
        public EthernetPortState EthernetPort4State { get; private set; }
        public EthernetPortSpeed EthernetPort4Speed { get; private set; }

        public VoIPLineState VoIPLine1State { get; private set; }
        public VoIPLineState VoIPLine2State { get; private set; }

        public H640GW02Onu(uint oltId, uint oltPortId, uint oltOnuId, IDataService snmpDataService) : base(oltId, oltPortId, oltOnuId, snmpDataService)
        {
            EthernetPort1State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort1State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort1Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort1Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.1").Result;
            EthernetPort2State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort2State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.2").Result;
            EthernetPort2Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort2Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.2").Result;
            EthernetPort3State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort3State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.3").Result;
            EthernetPort3Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort3Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.3").Result;
            EthernetPort4State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort4State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.4").Result;
            EthernetPort4Speed.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, EthernetPort4Speed.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1.4").Result;

            VoIPLine1State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, VoIPLine1State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".1").Result;
            VoIPLine2State.Value = _snmpDataService.GetOnuIntPropertyAsync(oltId, VoIPLine2State.SnmpOID + "." + oltPortId + "." + oltOnuId + ".2").Result;
        }
    }
}
