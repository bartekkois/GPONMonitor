using GPONMonitor.Exceptions;
using GPONMonitor.Models.Configuration;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GPONMonitor.Models.Olt
{
    public partial class Olt
    {
        private async Task<IList<Variable>> SnmpGetAsyncWithTimeout(VersionCode snmpVersion, string oid, int snmpRequestTimeout, SnmpV3Credentials snmpV3Credentials)
        {
            CancellationTokenSource cancellationTokenSource = new();
            Task timeoutTask = Task.Delay(snmpRequestTimeout);
            Task<IList<Variable>> task;

            if (await Task.WhenAny(task = SnmpGetAsync(snmpVersion, oid, snmpV3Credentials), timeoutTask) == timeoutTask)
            {
                cancellationTokenSource.Cancel();
                throw new SnmpTimeoutException(_localizer["SNMP request timeout"]);
            }

            return await task;
        }

        private async Task<IList<Variable>> SnmpGetAsync(VersionCode snmpVersion, string oid, SnmpV3Credentials snmpV3Credentials)
        {
            try
            {
                if (snmpVersion == VersionCode.V3)
                {
                    // SNMP v3 support
                    IPrivacyProvider privacyProvider = GetPrivicyProvider(snmpV3Credentials);
                    Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
                    ReportMessage report = discovery.GetResponse(SnmpTimeout, new IPEndPoint(SnmpIPAddress, SnmpPort));

                    GetRequestMessage getRequest = new(VersionCode.V3,
                                                Messenger.NextMessageId,
                                                Messenger.NextRequestId,
                                                new OctetString(snmpV3Credentials.AuthenticationUsername),
                                                OctetString.Empty,
                                                new List<Variable> { new Variable(new ObjectIdentifier(oid)) },
                                                privacyProvider,
                                                Messenger.MaxMessageSize,
                                                report);

                    ISnmpMessage getReply = await getRequest.GetResponseAsync(new IPEndPoint(SnmpIPAddress, SnmpPort));
                    return getReply.Variables();
                }
                else if (snmpVersion == VersionCode.V1 || snmpVersion == VersionCode.V2)
                {
                    // SNMP v1 and v2 support
                    Task<IList<Variable>> task = Messenger.GetAsync(snmpVersion,
                                        new IPEndPoint(SnmpIPAddress, SnmpPort),
                                        new OctetString(SnmpCommunity),
                                        new List<Variable> { new Variable(new ObjectIdentifier(oid)) });

                    return await task;
                }
                else
                    throw new Exception(_localizer["unsupported SNMP version"]);
            }
            catch (Exception exception)
            {
                throw new SnmpConnectionException(_localizer["SNMP request error: "] + exception.Message);
            }
        }

        private async Task<IList<Variable>> SnmpSetAsyncWithTimeout(VersionCode snmpVersion, string oid, ISnmpData data, int snmpRequestTimeout, SnmpV3Credentials snmpV3Credentials)
        {
            CancellationTokenSource cancellationTokenSource = new();
            Task timeoutTask = Task.Delay(snmpRequestTimeout);
            Task<IList<Variable>> task;

            if (await Task.WhenAny(task = SnmpSetAsync(snmpVersion, oid, data, snmpV3Credentials), timeoutTask) == timeoutTask)
            {
                cancellationTokenSource.Cancel();
                throw new SnmpTimeoutException(_localizer["SNMP request timeout"]);
            }

            return await task;
        }

        private async Task<IList<Variable>> SnmpSetAsync(VersionCode snmpVersion, string oid, ISnmpData data, SnmpV3Credentials snmpV3Credentials)
        {
            try
            {
                if (snmpVersion == VersionCode.V3)
                {
                    // SNMP v3 support
                    IPrivacyProvider privacyProvider = GetPrivicyProvider(snmpV3Credentials);
                    Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
                    ReportMessage report = discovery.GetResponse(SnmpTimeout, new IPEndPoint(SnmpIPAddress, SnmpPort));

                    SetRequestMessage setRequest = new(VersionCode.V3,
                                                Messenger.NextMessageId,
                                                Messenger.NextRequestId,
                                                new OctetString(snmpV3Credentials.AuthenticationUsername),
                                                OctetString.Empty,
                                                new List<Variable> { new Variable(new ObjectIdentifier(oid), data) },
                                                privacyProvider,
                                                Messenger.MaxMessageSize,
                                                report);

                    return (await setRequest.GetResponseAsync(new IPEndPoint(SnmpIPAddress, SnmpPort))).Variables();
                }
                else if (snmpVersion == VersionCode.V1 || snmpVersion == VersionCode.V2)
                {
                    // SNMP v1 and v2 support
                    Task<IList<Variable>> task = Messenger.SetAsync(snmpVersion,
                                        new IPEndPoint(SnmpIPAddress, SnmpPort),
                                        new OctetString(SnmpCommunity),
                                        new List<Variable> { new Variable(new ObjectIdentifier(oid), data) });

                    return await task;
                }
                else
                    throw new Exception(_localizer["unsupported SNMP version"]);
            }
            catch (Exception exception)
            {
                throw new SnmpConnectionException(_localizer["SNMP request error: "] + exception.Message);
            }
        }

        private async Task<List<Variable>> SnmpWalkAsyncWithTimeout(VersionCode snmpVersion, string oid, WalkMode walkMode, int snmpRequestTimeout, SnmpV3Credentials snmpV3Credentials)
        {
            CancellationTokenSource cancellationTokenSource = new();
            Task timeoutTask = Task.Delay(snmpRequestTimeout);
            Task<List<Variable>> task;

            if (await Task.WhenAny(task = SnmpWalkAsync(snmpVersion, oid, walkMode, snmpV3Credentials), timeoutTask) == timeoutTask)
            {
                cancellationTokenSource.Cancel();
                throw new SnmpTimeoutException(_localizer["SNMP request timeout"]);
            }

            return await task;
        }

        private async Task<List<Variable>> SnmpWalkAsync(VersionCode snmpVersion, string oid, WalkMode walkMode, SnmpV3Credentials snmpV3Credentials)
        {
            List<Variable> snmpWalkResult = new();

            try
            {
                if (snmpVersion == VersionCode.V3)
                {
                    // SNMP v3 support
                    IPrivacyProvider privacyProvider = GetPrivicyProvider(snmpV3Credentials);
                    Discovery discovery = Messenger.GetNextDiscovery(SnmpType.GetRequestPdu);
                    ReportMessage report = discovery.GetResponse(SnmpTimeout, new IPEndPoint(SnmpIPAddress, SnmpPort));

                    // GET BULK
                    await Messenger.BulkWalkAsync(VersionCode.V3,
                                                new IPEndPoint(SnmpIPAddress, SnmpPort),
                                                new OctetString(snmpV3Credentials.AuthenticationUsername),
                                                OctetString.Empty,
                                                new ObjectIdentifier(oid),
                                                snmpWalkResult,
                                                3,
                                                walkMode,
                                                privacyProvider,
                                                report);

                    return snmpWalkResult;
                }
                else if (snmpVersion == VersionCode.V1 || snmpVersion == VersionCode.V2)
                {
                    // SNMP v1 and v2 support
                    Task<int> taskWalk = Messenger.WalkAsync(snmpVersion,
                                        new IPEndPoint(SnmpIPAddress, SnmpPort),
                                        new OctetString(SnmpCommunity),
                                        new ObjectIdentifier(oid),
                                        snmpWalkResult,
                                        walkMode);

                    await taskWalk;
                    return snmpWalkResult;
                }
                else
                    throw new Exception(_localizer["unsupported SNMP version"]);
            }
            catch (Exception exception)
            {
                throw new SnmpConnectionException(_localizer["SNMP request error: "] + exception.Message);
            }
        }

        private static IPrivacyProvider GetPrivicyProvider(SnmpV3Credentials snmpV3Credentials)
        {
            IAuthenticationProvider authenticationProvider;
            IPrivacyProvider privacyProvider;

#pragma warning disable CS0618 // Type or member is obsolete
            if (snmpV3Credentials.AuthenticationType == "MD5")
                authenticationProvider = new MD5AuthenticationProvider(new OctetString(snmpV3Credentials.AuthenticationPassword));
            else if (snmpV3Credentials.AuthenticationType == "SHA1")
                authenticationProvider = new SHA1AuthenticationProvider(new OctetString(snmpV3Credentials.AuthenticationPassword));
            else
                authenticationProvider = new MD5AuthenticationProvider(new OctetString(snmpV3Credentials.AuthenticationPassword));

            if (snmpV3Credentials.EncryptionType == "DES")
                privacyProvider = new DESPrivacyProvider(new OctetString(snmpV3Credentials.EncryptionPassword), authenticationProvider);
            else if (snmpV3Credentials.EncryptionType == "AES")
                privacyProvider = new AESPrivacyProvider(new OctetString(snmpV3Credentials.EncryptionPassword), authenticationProvider);
            else
                privacyProvider = new AESPrivacyProvider(new OctetString(snmpV3Credentials.EncryptionPassword), authenticationProvider);
#pragma warning restore CS0618 // Type or member is obsolete

            return privacyProvider;
        }
    }
}
