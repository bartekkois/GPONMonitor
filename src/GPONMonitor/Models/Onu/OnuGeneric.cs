namespace GPONMonitor.Models.Onu
{
    public abstract class OnuGeneric
    {
        public int OltPortId { get; private set; }
        public int OltOnuId { get; private set; }
        public string Description { get; private set; }
        public string GponSerialNumber { get; private set; }
        public string ModelType { get; private set; }

        public OpticalConnectionState OpticalConnectionState { get; private set; }
        public OpticalConnectionDeactivationReason OpticalConnectionDeactivationReason { get; private set; }
        public OpticalPowerReceived OpticalPowerReceived { get; private set; }
        public int OpticalCabledDistance { get; private set; }

        public OpticalConnectionUptime OpticalConnectionUptime { get; private set; }
        public SystemUptime SystemUptime { get; private set; }

        public BlockStatus BlockStatus { get; private set; }
        public BlockReason BlockReason { get; private set; }


        // OLT SNMP OIDs
        const string snmpOIDOnuDescription = "1.3.6.1.4.1.6296.101.23.3.1.1.18";                      // Description (followed by OnuPortId and OnuId)
        const string snmpOIDOnuGponSerialNumber = "1.3.6.1.4.1.6296.101.23.3.1.1.4";                  // GPON Serial number (followed by OnuPortId and OnuId)
        const string snmpOIDOnuModelType = "1.3.6.1.4.1.6296.101.23.3.1.1.17";                        // Model type (followed by OnuPortId and OnuId)
        const string snmpOIDOnuOpticalConnectionState = "1.3.6.1.4.1.6296.101.23.3.1.1.2";            // Optical connection state (followed by OnuPortId and OnuId)
        const string snmpOIDOnuDeactivationReason = "1.3.6.1.4.1.6296.101.23.3.1.1.45";               // Deactivation Reason (followed by OnuPortId and OnuId)
        const string snmpOIDOnuOpticalCableDistance = "1.3.6.1.4.1.6296.101.23.3.1.1.10";             // Cable distance (followed by OnuPortId and OnuId)
        const string snmpOIDOnuOpticalPowerReceived = "1.3.6.1.4.1.6296.101.23.3.1.1.16";             // Optical power received (followed by OnuPortId and OnuId)

        const string snmpOIDOnuOpticalConnectionUptime = "1.3.6.1.4.1.6296.101.23.3.1.1.23";          // ONU optical connection uptime (the elapsed time after ont is up) (followed by OnuPortId and OnuId)
        const string snmpOIDOnuOpticalConnectionInactiveTime = "1.3.6.1.4.1.6296.101.23.3.1.1.85";    // ONU optical connections inactive time (the elapsed time after ont Inactive) (followed by OnuPortId and OnuId)
        const string snmpOIDOnuSystemUptime = "1.3.6.1.4.1.6296.101.23.3.1.1.61";                     // ONU system uptime (followed by OnuPortId and OnuId)
        const string snmpOIDOnuTimeUpdate1 = "1.3.6.1.4.1.6296.101.23.3.2.1";                         // ONU time update (set 21)
        const string snmpOIDOnuTimeUpdate2 = "1.3.6.1.4.1.6296.101.23.3.2.6";
        const string snmpOIDOnuTimeUpdate3 = "1.3.6.1.4.1.6296.101.23.3.2.7";
        const string snmpOIDOnuTimeUpdate4 = "1.3.6.1.4.1.6296.101.23.3.2.3";

        const string snmpOIDOnuBlockStatus = "1.3.6.1.4.1.6296.101.23.3.1.1.55";                      // Block status (followed by OnuPortId and OnuId)
        const string snmpOIDOnuBlockReason = "1.3.6.1.4.1.6296.101.23.3.1.1.56";                      // Block reason (followed by OnuPortId and OnuId)


        //public async Task<GenericOnu> GetDetailInfoAsync()
        //{
        //    await

        //    return this;
        //}
    }
}