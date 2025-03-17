using GPONMonitor.Exceptions;
using GPONMonitor.Models.Configuration;
using GPONMonitor.Services;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
        public SnmpV3Credentials SnmpV3Credentials { get; private set; }
        public int IpHostWebManagementPort { get; private set; }

        private readonly IOltFormatChecks _oltFormatChecks;
        private readonly IStringLocalizer<Olt> _localizer;

        public Olt(int id, string name, string snmpIPAddress, string snmpPort, string snmpVersion, string snmpCommunity, string snmpTimeout, SnmpV3Credentials snmpV3Credentials, string ipHostWebManagementPort, IOltFormatChecks oltFormatChecks, IStringLocalizer<Olt> localizer)
        {
            _oltFormatChecks = oltFormatChecks;
            _localizer = localizer;

            Id = oltFormatChecks.CheckOltIdFormat(id);
            Name = oltFormatChecks.CheckOltNameFormat(name);
            SnmpIPAddress = oltFormatChecks.CheckOltSnmpIpAddressFormat(snmpIPAddress);
            SnmpPort = oltFormatChecks.CheckOltSnmpPortFormat(snmpPort);
            SnmpVersion = oltFormatChecks.CheckOltSnmpVersionFormat(snmpVersion);
            SnmpCommunity = oltFormatChecks.CheckOltSnmpCommunityFormat(snmpCommunity);
            SnmpTimeout = oltFormatChecks.CheckOltSnmpTimeoutFormat(snmpTimeout);
            SnmpV3Credentials = oltFormatChecks.CheckSnmpV3Credentials(snmpVersion, snmpV3Credentials);
            IpHostWebManagementPort = oltFormatChecks.CheckIpHostWebManagementPortFormat(ipHostWebManagementPort);
        }

        public async Task<string> GetDescriptionAsync()
        {
            List<Variable> snmpResponseDescription = await SnmpGetAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOltDescription, SnmpTimeout, SnmpV3Credentials) as List<Variable>;

            if (snmpResponseDescription.Count == 0)
                throw new SnmpConnectionException(_localizer["SNMP request error: no result has been returned"]);

            return snmpResponseDescription.First().Data.ToString();
        }

        public async Task<string> GetUptimeAsync()
        {
            List<Variable> snmpResponseUptime = await SnmpGetAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOltUptime, SnmpTimeout, SnmpV3Credentials) as List<Variable>;

            if (snmpResponseUptime.Count == 0)
                throw new SnmpConnectionException(_localizer["SNMP request error: no result has been returned"]);

            return snmpResponseUptime.First().Data.ToString().Split('.').First();
        }

        public async Task<List<OnuShortDescription>> GetOnuDescriptionListAsync()
        {
            List<OnuShortDescription> onuList = new List<OnuShortDescription>();

            List<Variable> snmpOnuGponSerialNumberList = await SnmpWalkAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOnuGponSerialNumber, WalkMode.WithinSubtree, SnmpTimeout, SnmpV3Credentials);
            List<Variable> snmpOnuDescriptionList = await SnmpWalkAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOnuDescription, WalkMode.WithinSubtree, SnmpTimeout, SnmpV3Credentials);
            List<Variable> snmpOnuOpticalConnectionState = await SnmpWalkAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOnuOpticalConnectionState, WalkMode.WithinSubtree, SnmpTimeout, SnmpV3Credentials);
            List<Variable> snmpOnuOpticalPowerReceived = await SnmpWalkAsyncWithTimeout(SnmpVersion, SnmpOIDCollection.snmpOIDOnuOpticalPowerReceived, WalkMode.WithinSubtree, SnmpTimeout, SnmpV3Credentials);

            foreach (Variable variable in snmpOnuGponSerialNumberList)
            {
                uint oltPortId = variable.Id.ToNumerical().ToArray().ElementAt(13);
                uint onuId = variable.Id.ToNumerical().ToArray().ElementAt(14);

                string onuGponSerialNumber;
                if (variable.Data.ToString().Length > 0)
                    onuGponSerialNumber = variable.Data.ToString();
                else
                    onuGponSerialNumber = _localizer["unknown"];

                var relatedOnuDescription = snmpOnuDescriptionList.FirstOrDefault(x => x.Id.ToNumerical().ToArray().ElementAt(13) == oltPortId && x.Id.ToNumerical().ToArray().ElementAt(14) == onuId);
                string onuDescription;
                if (relatedOnuDescription != null)
                    onuDescription = relatedOnuDescription.Data.ToString().Replace("_", " ");
                else
                    onuDescription = _localizer["undefined"];

                var relatedOnuOpticalConnectionState = snmpOnuOpticalConnectionState.FirstOrDefault(x => x.Id.ToNumerical().ToArray().ElementAt(13) == oltPortId && x.Id.ToNumerical().ToArray().ElementAt(14) == onuId);
                string onuOpticalConnectionState;
                if (relatedOnuOpticalConnectionState != null)
                {
                    if (relatedOnuOpticalConnectionState.Data.ToString() == "2")
                        onuOpticalConnectionState = "up";
                    else
                        onuOpticalConnectionState = "down";
                }
                else
                {
                    onuOpticalConnectionState = "down";
                }

                var relatedOnuOpticalPowerReceived = snmpOnuOpticalPowerReceived.FirstOrDefault(x => x.Id.ToNumerical().ToArray().ElementAt(13) == oltPortId && x.Id.ToNumerical().ToArray().ElementAt(14) == onuId);
                string onuOpticalPowerReceived;
                if (relatedOnuOpticalPowerReceived != null)
                    onuOpticalPowerReceived = relatedOnuOpticalPowerReceived.Data.ToString();
                else
                    onuOpticalPowerReceived = _localizer["undefined"];

                onuList.Add(new OnuShortDescription(oltPortId, onuId, onuDescription, onuGponSerialNumber, onuOpticalConnectionState, onuOpticalPowerReceived));
            }

            return onuList;
        }

        public async Task<string> GetStringPropertyAsync(string snmpOid)
        {
            List<Variable> snmpResponse = await SnmpGetAsyncWithTimeout(SnmpVersion, snmpOid, SnmpTimeout, SnmpV3Credentials) as List<Variable>;

            if (snmpResponse.Count == 0)
                throw new SnmpConnectionException(_localizer["SNMP request error: no result has been returned"]);

            return snmpResponse.First().Data.ToString();
        }

        public async Task<int?> GetIntPropertyAsync(string snmpOid)
        {
            int parsedResult;
            List<Variable> snmpResponse = await SnmpGetAsyncWithTimeout(SnmpVersion, snmpOid, SnmpTimeout, SnmpV3Credentials) as List<Variable>;

            if (snmpResponse.Count == 0)
                throw new SnmpConnectionException(_localizer["SNMP request error: no result has been returned"]);

            if (int.TryParse(snmpResponse.First().Data.ToString(), out parsedResult) == false)
                return null;

            return parsedResult;
        }

        public async Task<IList<Variable>> SetStringPropertyAsync(string snmpOid, string data)
        {
            List<Variable> snmpResponse = await SnmpSetAsyncWithTimeout(SnmpVersion, snmpOid, new OctetString(data), SnmpTimeout, SnmpV3Credentials) as List<Variable>;

            if (snmpResponse.Count == 0)
                throw new SnmpConnectionException(_localizer["SNMP request error: no result has been returned"]);

            return snmpResponse;
        }

        public async Task<IList<Variable>> SetIntPropertyAsync(string snmpOid, int data)
        {
            List<Variable> snmpResponse = await SnmpSetAsyncWithTimeout(SnmpVersion, snmpOid, new Integer32(data), SnmpTimeout, SnmpV3Credentials) as List<Variable>;

            if (snmpResponse.Count == 0)
                throw new SnmpConnectionException(_localizer["SNMP request error: no result has been returned"]);

            return snmpResponse;
        }
    }
}
