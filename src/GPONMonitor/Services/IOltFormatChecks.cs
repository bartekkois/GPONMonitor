using GPONMonitor.Models.Configuration;
using Lextm.SharpSnmpLib;
using System.Net;

namespace GPONMonitor.Services
{
    public interface IOltFormatChecks
    {
        int CheckOltIdFormat(int id);
        string CheckOltNameFormat(string name);
        IPAddress CheckOltSnmpIpAddressFormat(string snmpIPAddress);
        int CheckOltSnmpPortFormat(string snmpPort);
        VersionCode CheckOltSnmpVersionFormat(string snmpVersion);
        string CheckOltSnmpCommunityFormat(string snmpCommunity);
        int CheckOltSnmpTimeoutFormat(string snmpTimeout);
        SnmpV3Credentials CheckSnmpV3Credentials(string snmpVersion, SnmpV3Credentials snmpV3Credentials);
        int CheckIpHostWebManagementPortFormat(string ipHostWebManagementPort);
    }
}
