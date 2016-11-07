using GPONMonitor.Services;
using Lextm.SharpSnmpLib;
using Microsoft.Extensions.Localization;
using System;
using System.Net;

namespace GPONMonitor.Models.Olt
{
    public class OltFormatChecks : IOltFormatChecks
    {
        private readonly IStringLocalizer<OltFormatChecks> _localizer;

        public OltFormatChecks(IStringLocalizer<OltFormatChecks> localizer)
        {
            _localizer = localizer;
        }

        public int CheckOltIdFormat(int id)
        {
            if ((id >= 0 && id <= 128))
                return id;
            else
                throw new ArgumentException(_localizer["Incorrect OLT id"]);
        }

        public string CheckOltNameFormat(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return name;
            else
                throw new ArgumentException("Incorrect OLT name");
        }

        public IPAddress CheckOltSnmpIpAddressFormat(string snmpIPAddress)
        {
            IPAddress tryParseSnmpIPAddress;
            if (IPAddress.TryParse(snmpIPAddress, out tryParseSnmpIPAddress))
                return tryParseSnmpIPAddress;
            else
                throw new ArgumentException("Incorrect OLT SNMP IP address");
        }

        public int CheckOltSnmpPortFormat(string snmpPort)
        {
            int tryParseSnmpPort;
            if (int.TryParse(snmpPort, out tryParseSnmpPort) && (tryParseSnmpPort > 0 && tryParseSnmpPort < 65535))
                return tryParseSnmpPort;
            else
                throw new ArgumentException("Incorrect OLT SNMP port number");
        }

        public VersionCode CheckOltSnmpVersionFormat(string snmpVersion)
        {
            switch (snmpVersion)
            {
                case "1":
                    return VersionCode.V1;
                case "2":
                    return VersionCode.V2;
                case "3":
                    return VersionCode.V3;
                default:
                    throw new ArgumentException("Incorrect OLT SNMP version");
            }
        }

        public string CheckOltSnmpCommunityFormat(string snmpCommunity)
        {
            if (!string.IsNullOrWhiteSpace(snmpCommunity))
                return snmpCommunity;
            else
                throw new ArgumentException("Incorrect OLT SNMP community");
        }

        public int CheckOltSnmpTimeoutFormat(string snmpTimeout)
        {
            int tryParseSnmpTimeout;
            if (int.TryParse(snmpTimeout, out tryParseSnmpTimeout) && (tryParseSnmpTimeout > 0 && tryParseSnmpTimeout < 60000))
                return tryParseSnmpTimeout;
            else
                throw new ArgumentException("Incorrect OLT SNMP timeout");
        }
    }
}
