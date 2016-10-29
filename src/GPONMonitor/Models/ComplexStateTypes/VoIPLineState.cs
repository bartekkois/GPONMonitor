using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class VoIPLineState
    {
        public int? Value
        {
            get
            {
                return Value;
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

                Value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }


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
            { null, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) },
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
            { 15, new ResponseDescription("configDone", "port skonfigurowany", SeverityLevel.Default) }
        };
    }
}