namespace GPONMonitor.Models.Onu
{
    public class H665GOnu : OnuGeneric
    {
        public EthernetPortState EthernetPort1State { get; private set; }
        public EthernetPortSpeed EthernetPort1Speed { get; private set; }


        const string snmpOIDOnuEthernetPort1Status = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";           // ETH port status (followed by OnuPortId, OnuId, 1 and PortNumber)
        const string snmpOIDOnuEthernetPort1Speed = "1.3.6.1.4.1.6296.101.23.6.1.1.1.8";            // ETH port speed (followed by OnuPortId, OnuId, 1 and PortNumber)
    }
}
