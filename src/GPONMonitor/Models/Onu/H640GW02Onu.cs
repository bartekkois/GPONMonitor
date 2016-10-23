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


        const string snmpOIDOnuEthernetPort1Status = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";           // ETH port status (followed by OnuPortId, OnuId, 1 and PortNumber)
        const string snmpOIDOnuEthernetPort2Status = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";
        const string snmpOIDOnuEthernetPort3Status = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";
        const string snmpOIDOnuEthernetPort4Status = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";
        const string snmpOIDOnuEthernetPort1Speed = "1.3.6.1.4.1.6296.101.23.6.1.1.1.8";            // ETH port speed (followed by OnuPortId, OnuId, 1 and PortNumber)
        const string snmpOIDOnuEthernetPort2Speed = "1.3.6.1.4.1.6296.101.23.6.1.1.1.8";
        const string snmpOIDOnuEthernetPort3Speed = "1.3.6.1.4.1.6296.101.23.6.1.1.1.8";
        const string snmpOIDOnuEthernetPort4Speed = "1.3.6.1.4.1.6296.101.23.6.1.1.1.8";
        const string snmpOIDOnuVoIPLine1Status = "1.3.6.1.4.1.6296.101.23.6.5.1.1.4";               // VoIP line status (followed by OnuPortId, OnuId and PortNumber)
        const string snmpOIDOnuVoIPLine2Status = "1.3.6.1.4.1.6296.101.23.6.5.1.1.4";
        const string snmpOIDOnuVoIPLineStatusUpdate1 = "1.3.6.1.4.1.6296.101.23.6.5.2.1";           // VoIP line status update
        const string snmpOIDOnuVoIPLineStatusUpdate2 = "1.3.6.1.4.1.6296.101.23.6.5.2.6";
        const string snmpOIDOnuVoIPLineStatusUpdate3 = "1.3.6.1.4.1.6296.101.23.6.5.2.7";
        const string snmpOIDOnuVoIPLineStatusUpdate4 = "1.3.6.1.4.1.6296.101.23.6.5.2.3";
    }
}
