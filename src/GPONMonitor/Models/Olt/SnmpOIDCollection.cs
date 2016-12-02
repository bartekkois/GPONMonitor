namespace GPONMonitor.Models.Olt
{
    public static class SnmpOIDCollection
    {
        // OLT SNMP OIDs
        public const string snmpOIDOltDescription = "1.3.6.1.2.1.1.1.0";
        public const string snmpOIDOltUptime = "1.3.6.1.2.1.1.3.0";

        // ONU SNMP OIDs
        public const string snmpOIDGetOnuModelType = "1.3.6.1.4.1.6296.101.23.3.1.1.17";                            // Model Type (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuDescription = "1.3.6.1.4.1.6296.101.23.3.1.1.18";                             // Description (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuGponSerialNumber = "1.3.6.1.4.1.6296.101.23.3.1.1.4";                         // Gpon Serial Number (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuGponProfile = "1.3.6.1.4.1.6296.101.23.3.1.1.8";                              // Gpon profile (followed by OnuPortId and OnuId)

        public const string snmpOIDOnuOpticalConnectionState = "1.3.6.1.4.1.6296.101.23.3.1.1.2";                   // Optical connection state (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuOpticalConnectionDeactivationReason = "1.3.6.1.4.1.6296.101.23.3.1.1.45";     // Deactivation Reason (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuOpticalPowerReceived = "1.3.6.1.4.1.6296.101.23.3.1.1.16";                    // Optical power received (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuOpticalCabelDistance = "1.3.6.1.4.1.6296.101.23.3.1.1.10";                    // Optical cable distance (followed by OnuPortId and OnuId)

        public const string snmpOIDOnuOpticalConnectionUptime = "1.3.6.1.4.1.6296.101.23.3.1.1.23";                 // Optical connection uptime (the elapsed time after ont is up) (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuOpticalConnectionInactiveTime = "1.3.6.1.4.1.6296.101.23.3.1.1.85";              // Optical connection inactive time (the elapsed time after ont Inactive) (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuSystemUptime = "1.3.6.1.4.1.6296.101.23.3.1.1.61";                               // System uptime (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuTimersUpdate1 = "1.3.6.1.4.1.6296.101.23.3.2.1";                              // Timers update (set 21)
        public const string snmpOIDOnuTimersUpdate2 = "1.3.6.1.4.1.6296.101.23.3.2.6";                              // Timers update (set OltPortId)
        public const string snmpOIDOnuTimersUpdate3 = "1.3.6.1.4.1.6296.101.23.3.2.7";                              // Timers update (set OnuId)
        public const string snmpOIDOnuTimersUpdate4 = "1.3.6.1.4.1.6296.101.23.3.2.3";                              // Timers update (set 0)

        public const string snmpOIDOnuBlockStatus = "1.3.6.1.4.1.6296.101.23.3.1.1.55";                             // Block status (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuBlockReason = "1.3.6.1.4.1.6296.101.23.3.1.1.56";                             // Block reason (followed by OnuPortId and OnuId)

        public const string snmpOIDOnuEthernetPortState = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";                      // ETH port state (followed by OnuPortId, OnuId, 1 and PortNumber)
        public const string snmpOIDOnuEthernetPortSpeed = "1.3.6.1.4.1.6296.101.23.6.1.1.1.8";                      // ETH port speed (followed by OnuPortId, OnuId, 1 and PortNumber)

        public const string snmpOIDOnuVoIPLineState = "1.3.6.1.4.1.6296.101.23.6.5.1.1.4";                          // Voip line state (followed by OnuPortId and OnuId)
        public const string snmpOIDOnuVoIPLineStateUpdate1 = "1.3.6.1.4.1.6296.101.23.6.5.2.1";                     // Voip line state update (set 1)
        public const string snmpOIDOnuVoIPLineStateUpdateOltPortId = "1.3.6.1.4.1.6296.101.23.6.5.2.6";             // Voip line state update (set OltPortId)
        public const string snmpOIDOnuVoIPLineStateUpdateOnuId = "1.3.6.1.4.1.6296.101.23.6.5.2.7";                 // Voip line state update (set OnuId)
        public const string snmpOIDOnuVoIPLineStateUpdate0 = "1.3.6.1.4.1.6296.101.23.6.5.2.3";                     // Voip line state (set 0)
    }
}
