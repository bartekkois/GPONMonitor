using GPONMonitor.Models;
using GPONMonitor.Models.ComplexStateTypes;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace GPONMonitor.Services
{
    public class ResponseDescriptionDictionaries : IResponseDescriptionDictionaries
    {
        private readonly IStringLocalizer<ResponseDescriptionDictionaries> _localizer;

        readonly Dictionary<int, ResponseDescription> OpticalConnectionStateResponseDictionary;
        readonly Dictionary<int, ResponseDescription> OpticalConnectionDeactivationReasonResponseDictionary;
        readonly Dictionary<int?, ResponseDescription> BlockStatusResponseDictionary;
        readonly Dictionary<int?, ResponseDescription> BlockReasonResponseDictionary;
        readonly Dictionary<int, ResponseDescription> EthernetPortStateResponseDictionary;
        readonly Dictionary<int, ResponseDescription> EthernetPortSpeedResponseDictionary;
        readonly Dictionary<int, ResponseDescription> VoIPLinestatusResponseDictionary;

        private const int _unknownResponseCode = 255;


        public ResponseDescriptionDictionaries(IStringLocalizer<ResponseDescriptionDictionaries> localizer)
        {
            _localizer = localizer;

            // ONU Link Status
            // 0 - invalid
            // 1 - inactive
            // 2 - active
            // 3 - running (OBSOLETE: removed in 5.08)

            OpticalConnectionStateResponseDictionary = new Dictionary<int, ResponseDescription>()
            {
                { 0, new ResponseDescription(_localizer["invalid"], SeverityLevel.Danger) },
                { 1, new ResponseDescription(_localizer["inactive"], SeverityLevel.Danger) },
                { 2, new ResponseDescription(_localizer["active"], SeverityLevel.Success) },
                { 3, new ResponseDescription(_localizer["running"], SeverityLevel.Success) },
                { 255, new ResponseDescription(_localizer["unknown"], SeverityLevel.Success) }
            };

            // ONU Deactivation Reason
            // none(1),
            // dgi(2),
            // losi(3),
            // lofi(4),
            // sfi(5),
            // sufi(6),
            // loai(7),
            // loami(8),
            // loki(9),
            // adminReset(10),
            // adminActiveChange(11),
            // adminOltConfiguration(12),
            // adminSlotRestart(13),
            // adminSlotRemove(14),
            // adminRogueOnuCandidate(15),
            // adminRogueOnu(16),
            // adminRogueOnuSelfDetectBlock(17),
            // adminTxOffOptic(18),
            // adminDeactivate(19),
            // adminOltDeactivate(20),
            // adminOmccDown(21),
            // adminSetRedundancy(22),
            // adminRemoveOnu(23),
            // los(100),
            // unknown(255)

            OpticalConnectionDeactivationReasonResponseDictionary = new Dictionary<int, ResponseDescription>()
            {
                { 1, new ResponseDescription(_localizer["none"], SeverityLevel.Danger) },
                { 2, new ResponseDescription(_localizer["dying gasp (DGI)"], SeverityLevel.Danger) },
                { 3, new ResponseDescription(_localizer["loss of signal (LOSI)"], SeverityLevel.Success) },
                { 4, new ResponseDescription(_localizer["loss of frame (LOFI)"], SeverityLevel.Success) },
                { 5, new ResponseDescription(_localizer["signal fail (SFI)"], SeverityLevel.Danger) },
                { 6, new ResponseDescription(_localizer["start-up failure (SUFI)"], SeverityLevel.Danger) },
                { 7, new ResponseDescription(_localizer["loss of PLOAM (LOAI)"], SeverityLevel.Success) },
                { 8, new ResponseDescription(_localizer["loss of PLOAM (LOAMI)"], SeverityLevel.Success) },
                { 9, new ResponseDescription(_localizer["loss of key sync (LOKI)"], SeverityLevel.Danger) },
                { 10, new ResponseDescription(_localizer["admin reset"], SeverityLevel.Success) },
                { 11, new ResponseDescription(_localizer["admin active change"], SeverityLevel.Success) },
                { 12, new ResponseDescription(_localizer["admin olt configuration"], SeverityLevel.Danger) },
                { 13, new ResponseDescription(_localizer["admin slot restart"], SeverityLevel.Danger) },
                { 14, new ResponseDescription(_localizer["admin slot remove"], SeverityLevel.Success) },
                { 15, new ResponseDescription(_localizer["admin rogue onu candidate"], SeverityLevel.Success) },
                { 16, new ResponseDescription(_localizer["admin rogue onu"], SeverityLevel.Danger) },
                { 17, new ResponseDescription(_localizer["admin rogue onu self detect block"], SeverityLevel.Danger) },
                { 18, new ResponseDescription(_localizer["admin tx off optic"], SeverityLevel.Success) },
                { 19, new ResponseDescription(_localizer["admin deactivate"], SeverityLevel.Success) },
                { 20, new ResponseDescription(_localizer["admin olt deactivate"], SeverityLevel.Danger) },
                { 21, new ResponseDescription(_localizer["admin omcc down"], SeverityLevel.Success) },
                { 22, new ResponseDescription(_localizer["admin set redundancy"], SeverityLevel.Success) },
                { 23, new ResponseDescription(_localizer["admin remove onu"], SeverityLevel.Danger) },
                { 100, new ResponseDescription(_localizer["loss of signal (LOS)"], SeverityLevel.Danger) },
                { 255, new ResponseDescription(_localizer["unknown"], SeverityLevel.Success) }
            };

            // ONT Block Status
            // 1 - autoblock
            // 2 - manual block
            // 255 - unblock

            BlockStatusResponseDictionary = new Dictionary<int?, ResponseDescription>()
            {
                { 1, new ResponseDescription(_localizer["autoblock"], SeverityLevel.Danger) },
                { 2, new ResponseDescription(_localizer["manual block"], SeverityLevel.Danger) },
                { 255, new ResponseDescription(_localizer["unblock"], SeverityLevel.Success) }
            };

            // ONT Block Reason
            // 1 - manual block
            // 2 - sourcemac block
            // 255 - unblock

            BlockReasonResponseDictionary = new Dictionary<int?, ResponseDescription>()
            {
                { 1, new ResponseDescription(_localizer["manual block"], SeverityLevel.Danger) },
                { 2, new ResponseDescription(_localizer["sourcemac block"], SeverityLevel.Danger) },
                { 255, new ResponseDescription(_localizer["unblock"], SeverityLevel.Success) }
            };

            // ONT Ethernet Port State
            // 1 - manual block
            // 2 - sourcemac block

            EthernetPortStateResponseDictionary = new Dictionary<int, ResponseDescription>()
            {
                { 1, new ResponseDescription(_localizer["up"], SeverityLevel.Success) },
                { 2, new ResponseDescription(_localizer["down"], SeverityLevel.Warning) },
                { 255, new ResponseDescription(_localizer["unknown"], SeverityLevel.Unknown) }
            };

            // ONT Ethernet Port Speed
            // 1 - 10 Mb/s
            // 2 - 100 Mb/s
            // 3 - 1000 Mb/s

            EthernetPortSpeedResponseDictionary = new Dictionary<int, ResponseDescription>()
            {
                { 1, new ResponseDescription(_localizer["10 Mb/s"], SeverityLevel.Success) },
                { 2, new ResponseDescription(_localizer["100 Mb/s"], SeverityLevel.Success) },
                { 3, new ResponseDescription(_localizer["1000 Mb/s"], SeverityLevel.Success) },
                { 255, new ResponseDescription(_localizer["unknown"], SeverityLevel.Unknown) }
            };

            // ONT VoIP Line Status
            // noneInitial(1),
            // registered(2),
            // inSession(3),
            // failedRegistrationIcmpError(4),
            // failedRegistrationFailedTcp(5),
            // failedRegistrationFailedAuthentication(6),
            // failedRegistrationTimeout(7),
            // failedRegistrationServerFailCode(8),
            // failedInviteIcmpError(9),
            // failedInviteFailedTcp(10),
            // failedInviteFailedAuthentication(11),
            // failedInviteTimeout(12),
            // failedInviteServerFailCode(13),
            // portNotConfigured(14),
            // configDone(15)

            VoIPLinestatusResponseDictionary = new Dictionary<int, ResponseDescription>()
            {
                { 1, new ResponseDescription(_localizer["none initial"], SeverityLevel.Warning) },
                { 2, new ResponseDescription(_localizer["registered"], SeverityLevel.Success) },
                { 3, new ResponseDescription(_localizer["in session"], SeverityLevel.Success) },
                { 4, new ResponseDescription(_localizer["failed registration (ICMP error)"], SeverityLevel.Danger) },
                { 5, new ResponseDescription(_localizer["failed registration (failed TCP)"], SeverityLevel.Danger) },
                { 6, new ResponseDescription(_localizer["failed registration (failed authentication)"], SeverityLevel.Danger) },
                { 7, new ResponseDescription(_localizer["failed registration (timeout)"], SeverityLevel.Danger) },
                { 8, new ResponseDescription(_localizer["failed registration (server fail code)"], SeverityLevel.Danger) },
                { 9, new ResponseDescription(_localizer["failed invite (ICMP error)"], SeverityLevel.Danger) },
                { 10, new ResponseDescription(_localizer["failed invite (failed TCP)"], SeverityLevel.Danger) },
                { 11, new ResponseDescription(_localizer["failed invite (failed authentication)"], SeverityLevel.Danger) },
                { 12, new ResponseDescription(_localizer["failed invite (timeout)"], SeverityLevel.Danger) },
                { 13, new ResponseDescription(_localizer["failed invite (server fail code)"], SeverityLevel.Danger) },
                { 14, new ResponseDescription(_localizer["port not configured"], SeverityLevel.Warning) },
                { 15, new ResponseDescription(_localizer["config done"],  SeverityLevel.Success) },
                { 255, new ResponseDescription(_localizer["unknown"], SeverityLevel.Unknown) }
            };

        }


        ResponseDescription IResponseDescriptionDictionaries.OpticalConnectionStateResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (OpticalConnectionStateResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                OpticalConnectionStateResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                OpticalConnectionStateResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }



        ResponseDescription IResponseDescriptionDictionaries.OpticalConnectionDeactivationReasonResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (OpticalConnectionDeactivationReasonResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.BlockStatusResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (BlockStatusResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                BlockStatusResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                BlockStatusResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.BlockReasonResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (BlockReasonResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                BlockReasonResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                BlockReasonResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.EthernetPortStateResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (EthernetPortStateResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                EthernetPortStateResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                EthernetPortStateResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.EthernetPortSpeedResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (EthernetPortSpeedResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                EthernetPortSpeedResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                EthernetPortSpeedResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.VoIPLinestatusResponse(int? responseCode)
        {
            ResponseDescription _responseDescription;

            if (VoIPLinestatusResponseDictionary.ContainsKey(ToDictionaryCode(responseCode)))
            {
                VoIPLinestatusResponseDictionary.TryGetValue(ToDictionaryCode(responseCode), out _responseDescription);
            }
            else
            {
                VoIPLinestatusResponseDictionary.TryGetValue(_unknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        private static int ToDictionaryCode(int? responseCode)
        {
            if (responseCode.HasValue)
                return responseCode.Value;
            else
                return _unknownResponseCode;
        }
    }
}
