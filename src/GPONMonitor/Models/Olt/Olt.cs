using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GPONMonitor.Exceptions;
using GPONMonitor.Services;

namespace GPONMonitor.Models.Olt
{
    public partial class Olt
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IPAddress SnmpIPAddress { get; private set; }
        public int SnmpPort { get; private set; }
        public VersionCode SnmpVersion { get; private set; }
        public string SnmpCommunity { get; private set; }
        public int SnmpTimeout { get; private set; }

        private readonly IOltFormatChecks _oltFormatChecks;

        public Olt(int id, string name, string snmpIPAddress, string snmpPort, string snmpVersion, string snmpCommunity, string snmpTimeout, IOltFormatChecks oltFormatChecks)
        {
            _oltFormatChecks = oltFormatChecks;

            Id = oltFormatChecks.CheckOltIdFormat(id);
            Name = oltFormatChecks.CheckOltNameFormat(name);
            SnmpIPAddress = oltFormatChecks.CheckOltSnmpIpAddressFormat(snmpIPAddress);
            SnmpPort = oltFormatChecks.CheckOltSnmpPortFormat(snmpPort);
            SnmpVersion = oltFormatChecks.CheckOltSnmpVersionFormat(snmpVersion);
            SnmpCommunity = oltFormatChecks.CheckOltSnmpCommunityFormat(snmpCommunity);
            SnmpTimeout = oltFormatChecks.CheckOltSnmpTimeoutFormat(snmpTimeout);
        }

        public async Task<string> GetDescriptionAsync()
        {
            List<Variable> snmpResponseDescription = await SnmpGetAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOltDescription, SnmpTimeout) as List<Variable>;

            if (snmpResponseDescription.Count == 0)
                throw new SnmpConnectionException("SNMP request error: no result has been returned");

            return snmpResponseDescription.First().Data.ToString();
        }

        public async Task<string> GetUptimeAsync()
        {
            List<Variable> snmpResponseUptime = await SnmpGetAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOltUptime, SnmpTimeout) as List<Variable>;

            if (snmpResponseUptime.Count == 0)
                throw new SnmpConnectionException("SNMP request error: no result has been returned");

            return snmpResponseUptime.First().Data.ToString().Split('.').First();
        }

        public async Task<List<OnuShortDescription>> GetOnuDescriptionListAsync()
        {
            List<OnuShortDescription> onuList = new List<OnuShortDescription>();

            List<Variable> snmpOnuGponSerialNumberList = await SnmpWalkAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOnuGponSerialNumber, SnmpTimeout, WalkMode.WithinSubtree, SnmpTimeout);
            List<Variable> snmpOnuDescriptionList = await SnmpWalkAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOnuDescription, SnmpTimeout, WalkMode.WithinSubtree, SnmpTimeout);

            foreach (Variable variable in snmpOnuGponSerialNumberList)
            {
                uint oltPortId = variable.Id.ToNumerical().ToArray().ElementAt(13);
                uint onuId = variable.Id.ToNumerical().ToArray().ElementAt(14);

                string onuGponSerialNumber;
                if (variable.Data.ToString().Length > 0)
                    onuGponSerialNumber = variable.Data.ToString();
                else
                    onuGponSerialNumber = "unknown";

                var relatedOnuDescription = snmpOnuDescriptionList.FirstOrDefault(x => x.Id.ToNumerical().ToArray().ElementAt(13) == oltPortId && x.Id.ToNumerical().ToArray().ElementAt(14) == onuId);

                string onuDescription;
                if (relatedOnuDescription != null)
                    onuDescription = relatedOnuDescription.Data.ToString().Replace("_", " ");
                else
                    onuDescription = "undefined";

                onuList.Add(new OnuShortDescription(oltPortId, onuId, onuDescription, onuGponSerialNumber));
            }

            return onuList;
        }

        public async Task<string> GetStringPropertyAsync(string snmpOid)
        {
            List<Variable> snmpResponse = await SnmpGetAsyncWithTimeout(SnmpVersion, snmpOid, SnmpTimeout) as List<Variable>;

            if (snmpResponse.Count == 0)
                throw new SnmpConnectionException("SNMP request error: no result has been returned");

            return snmpResponse.First().Data.ToString();
        }

        public async Task<int> GetIntPropertyAsync(string snmpOid)
        {
            int parsedResult;
            List<Variable> snmpResponse = await SnmpGetAsyncWithTimeout(SnmpVersion, snmpOid, SnmpTimeout) as List<Variable>;

            if (snmpResponse.Count == 0)
                throw new SnmpConnectionException("SNMP request error: no result has been returned");

            if (int.TryParse(snmpResponse.First().Data.ToString(), out parsedResult) == false)
                throw new SnmpConnectionException("SNMP request error: wrong format result has been returned");

            return parsedResult; 
        }
    }
}
