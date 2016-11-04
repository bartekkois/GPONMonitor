using Newtonsoft.Json;
using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class VoIPLineState
    {
        private int? value;
        public int? Value
        {
            get
            {
                return value;
            }
            set
            {
                ResponseDescription responseDescription;

                if (VoIPLinestatusResponseDictionary.ContainsKey(value))
                {
                    VoIPLinestatusResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    VoIPLinestatusResponseDictionary.TryGetValue(null, out responseDescription);
                }

                DescriptionEng = responseDescription.DescriptionEng;
                DescriptionPol = responseDescription.DescriptionPol;
                Severity = responseDescription.Severity;
                this.value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.6.5.1.1.4";                  // ONU system uptime (followed by OnuPortId and OnuId)
        [JsonIgnore]
        public string SnmpOIDOnuVoipLineUpdate1 { get; private set; } = "1.3.6.1.4.1.6296.101.23.6.5.2.1";  // ONU time update (set 1)
        [JsonIgnore]
        public string SnmpOIDOnuVoipLineUpdate2 { get; private set; } = "1.3.6.1.4.1.6296.101.23.6.5.2.6";  // ONU time update (set OltPortId)
        [JsonIgnore]
        public string SnmpOIDOnuVoipLineUpdate3 { get; private set; } = "1.3.6.1.4.1.6296.101.23.6.5.2.7";  // ONU time update (set OnuId)
        [JsonIgnore]
        public string SnmpOIDOnuVoipLineUpdate4 { get; private set; } = "1.3.6.1.4.1.6296.101.23.6.5.2.3";  // ONU time update (set 0)


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

        readonly Dictionary<int?, ResponseDescription> VoIPLinestatusResponseDictionary = new Dictionary<int?, ResponseDescription>()
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
    }
}