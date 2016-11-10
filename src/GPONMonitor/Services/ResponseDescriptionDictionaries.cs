using GPONMonitor.Models;
using GPONMonitor.Models.ComplexStateTypes;
using System.Collections.Generic;

namespace GPONMonitor.Services
{
    public class ResponseDescriptionDictionaries : IResponseDescriptionDictionaries
    {
        private const int _uknownResponseCode = 255;

        // ONU Link Status
        // 0 - invalid
        // 1 - inactive
        // 2 - active
        // 3 - running (OBSOLETE: removed in 5.08)

        readonly Dictionary<int, ResponseDescription> OpticalConnectionStateResponseDictionary = new Dictionary<int, ResponseDescription>()
        {
            { 0, new ResponseDescription("invalid", "niepoprawne", SeverityLevel.Danger) },
            { 1, new ResponseDescription("inactive", "nieaktywne", SeverityLevel.Danger) },
            { 2, new ResponseDescription("active", "aktywne", SeverityLevel.Success) },
            { 3, new ResponseDescription("running", "działające", SeverityLevel.Success) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Success) }
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

        readonly Dictionary<int, ResponseDescription> OpticalConnectionDeactivationReasonResponseDictionary = new Dictionary<int, ResponseDescription>()
        {
            { 1, new ResponseDescription("none", "brak powodu", SeverityLevel.Danger) },
            { 2, new ResponseDescription("dying gasp (DGI)", "zanik zasilania (DGi)", SeverityLevel.Danger) },
            { 3, new ResponseDescription("loss of signal (LOSI)", "uszk. kabel magistralny (LOSI)", SeverityLevel.Success) },    // TO BE  FINISHED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            { 4, new ResponseDescription("loss of frame (LOFI)", "uszk. kabel abonencki (LOFI)", SeverityLevel.Success) },      // TO BE  FINISHED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            { 5, new ResponseDescription("signal fail (SFI)", "błędy transmisji (SFI)", SeverityLevel.Danger) },
            { 6, new ResponseDescription("start-up failure (SUFI)", "błędy inicjalizacji (SUFI)", SeverityLevel.Danger) },
            { 7, new ResponseDescription("loss of PLOAM (LOAI)", "utrata PLOAM (LOAI)", SeverityLevel.Success) },
            { 8, new ResponseDescription("loss of PLOAM (LOAMI)", "utrata PLOAM (LOAMI)", SeverityLevel.Success) },
            { 9, new ResponseDescription("loss of key sync (LOKI)", "utrata klucza (LOKI)", SeverityLevel.Danger) },
            { 10, new ResponseDescription("admin reset", "reset (administrator)", SeverityLevel.Success) },
            { 11, new ResponseDescription("admin active change", "zamina na aktywny (administrator)", SeverityLevel.Success) },
            { 12, new ResponseDescription("admin olt configuration", "konfiguracja olt (administrator)", SeverityLevel.Danger) },
            { 13, new ResponseDescription("admin slot restart", "restart slotu (administrator)", SeverityLevel.Danger) },
            { 14, new ResponseDescription("admin slot remove", "usunięcie slotu (administrator)", SeverityLevel.Success) },
            { 15, new ResponseDescription("admin rogue onu candidate", "uszkodzony modem (kandydujący)(administrator)", SeverityLevel.Success) },
            { 16, new ResponseDescription("admin rogue onu", "uszkodzony modem (administator)", SeverityLevel.Danger) },
            { 17, new ResponseDescription("admin rogue onu self detect block", "uszkodzony modem (automatyczna blokada) (administrator)", SeverityLevel.Danger) },
            { 18, new ResponseDescription("admin tx off optic", "laser wyłączony (administrator)", SeverityLevel.Success) },
            { 19, new ResponseDescription("admin deactivate", "deaktywacja (administrator)", SeverityLevel.Success) },
            { 20, new ResponseDescription("admin olt deactivate", "deaktyacja olt (administrator)", SeverityLevel.Danger) },
            { 21, new ResponseDescription("admin omcc down", "deaktywacja omcc (administrator)", SeverityLevel.Success) },
            { 22, new ResponseDescription("admin set redundancy", "ustawienie redundancji (administrator)", SeverityLevel.Success) },
            { 23, new ResponseDescription("admin remove onu", "usunięcie onu (administrator)", SeverityLevel.Danger) },
            { 100, new ResponseDescription("loss of signal (LOS)", "zanik sygału portu PON (LOS)", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Success) }
        };

        // ONT Block Status
        // 1 - autoblock
        // 2 - manual block
        // 255 - unblock

        readonly Dictionary<int?, ResponseDescription> BlockStatusResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 1, new ResponseDescription("autoblock", "blokada automatyczna", SeverityLevel.Danger) },
            { 2, new ResponseDescription("manual block", "blokada ręczna", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unblock", "brak blokady", SeverityLevel.Success) }
        };

        // ONT Block Reason
        // 1 - manual block
        // 2 - sourcemac block
        // 255 - unblock

        readonly Dictionary<int?, ResponseDescription> BlockReasonResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 1, new ResponseDescription("manual block", "blokada ręczna", SeverityLevel.Danger) },
            { 2, new ResponseDescription("sourcemac block", "blokada sourcemac", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unblock", "brak blokady", SeverityLevel.Success) }
        };

        // ONT Ethernet Port State
        // 1 - manual block
        // 2 - sourcemac block

        readonly Dictionary<int, ResponseDescription> EthernetPortStateResponseDictionary = new Dictionary<int, ResponseDescription>()
        {
            { 1, new ResponseDescription("up", "podniesiony", SeverityLevel.Danger) },
            { 2, new ResponseDescription("down", "opuszczony", SeverityLevel.Success) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) }
        };

        // ONT Ethernet Port Speed
        // 1 - 10 Mb/s
        // 2 - 100 Mb/s
        // 3 - 1000 Mb/s

        readonly Dictionary<int, ResponseDescription> EthernetPortSpeedResponseDictionary = new Dictionary<int, ResponseDescription>()
        {
            { 1, new ResponseDescription("10 Mb/s", "10 Mb/s", SeverityLevel.Success) },
            { 2, new ResponseDescription("100 Mb/s", "100 Mb/s", SeverityLevel.Success) },
            { 3, new ResponseDescription("1000 Mb/s", "1000 Mb/s", SeverityLevel.Success) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) }
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

        readonly Dictionary<int, ResponseDescription> VoIPLinestatusResponseDictionary = new Dictionary<int, ResponseDescription>()
        {
            { 1, new ResponseDescription("noneInitial", "niezarejestrowany", SeverityLevel.Default) },
            { 2, new ResponseDescription("registered", "zarejestrowany", SeverityLevel.Success) },
            { 3, new ResponseDescription("inSession", "rozmowa", SeverityLevel.Info) },
            { 4, new ResponseDescription("failedRegistrationIcmpError", "błąd rejestracji", SeverityLevel.Danger) },
            { 5, new ResponseDescription("failedRegistrationFailedTcp", "błąd rejestracji", SeverityLevel.Danger) },
            { 6, new ResponseDescription("failedRegistrationFailedAuthentication", "błąd rejestracji", SeverityLevel.Danger) },
            { 7, new ResponseDescription("failedRegistrationTimeout", "błąd rejestracji", SeverityLevel.Danger) },
            { 8, new ResponseDescription("failedRegistrationServerFailCode", "błąd rejestracji", SeverityLevel.Danger) },
            { 9, new ResponseDescription("failedInviteIcmpError", "błąd rejestracji", SeverityLevel.Danger) },
            { 10, new ResponseDescription("failedInviteFailedTcp", "błąd rejestracji", SeverityLevel.Danger) },
            { 11, new ResponseDescription("failedInviteFailedAuthentication", "błąd rejestracji", SeverityLevel.Danger) },
            { 12, new ResponseDescription("failedInviteTimeout", "błąd rejestracji", SeverityLevel.Danger) },
            { 13, new ResponseDescription("failedInviteServerFailCode", "błąd rejestracji", SeverityLevel.Danger) },
            { 14, new ResponseDescription("portNotConfigured", "port nieskonfigurowany", SeverityLevel.Default) },
            { 15, new ResponseDescription("configDone", "port skonfigurowany", SeverityLevel.Default) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Success) }
        };


        ResponseDescription IResponseDescriptionDictionaries.OpticalConnectionStateResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (OpticalConnectionStateResponseDictionary.ContainsKey(responseCode))
            {
                OpticalConnectionStateResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                OpticalConnectionStateResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.OpticalConnectionDeactivationReasonResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (OpticalConnectionDeactivationReasonResponseDictionary.ContainsKey(responseCode))
            {
                OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.BlockStatusResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (BlockStatusResponseDictionary.ContainsKey(responseCode))
            {
                BlockStatusResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                BlockStatusResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.BlockReasonResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (BlockReasonResponseDictionary.ContainsKey(responseCode))
            {
                BlockReasonResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                BlockReasonResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.EthernetPortStateResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (EthernetPortStateResponseDictionary.ContainsKey(responseCode))
            {
                EthernetPortStateResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                EthernetPortStateResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.EthernetPortSpeedResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (EthernetPortSpeedResponseDictionary.ContainsKey(responseCode))
            {
                EthernetPortSpeedResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                EthernetPortSpeedResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }

        ResponseDescription IResponseDescriptionDictionaries.VoIPLinestatusResponse(int responseCode)
        {
            ResponseDescription _responseDescription;

            if (VoIPLinestatusResponseDictionary.ContainsKey(responseCode))
            {
                VoIPLinestatusResponseDictionary.TryGetValue(responseCode, out _responseDescription);
            }
            else
            {
                VoIPLinestatusResponseDictionary.TryGetValue(_uknownResponseCode, out _responseDescription);
            }

            return _responseDescription;
        }
    }
}
