using Newtonsoft.Json;
using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalConnectionDeactivationReason
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

                if (OpticalConnectionDeactivationReasonResponseDictionary.ContainsKey(value))
                {
                    OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    OpticalConnectionDeactivationReasonResponseDictionary.TryGetValue(null, out responseDescription);
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

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.1.1.45";               // Deactivation Reason (followed by OnuPortId and OnuId)


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

        [JsonIgnore]
        readonly Dictionary<int?, ResponseDescription> OpticalConnectionDeactivationReasonResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { null, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) },
            { 1, new ResponseDescription("none", "brak powodu", SeverityLevel.Danger) },
            { 2, new ResponseDescription("dgi", "brak zasilania", SeverityLevel.Danger) },
            { 3, new ResponseDescription("losi", "uszk. kabel magistralny", SeverityLevel.Success) },
            { 4, new ResponseDescription("lofi", "uszk. kabel abonencki", SeverityLevel.Success) },
            { 5, new ResponseDescription("sfi", "błędy transmisji", SeverityLevel.Danger) },
            { 6, new ResponseDescription("sufi", "", SeverityLevel.Danger) },        // TO BE  FINISHED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            { 7, new ResponseDescription("loai", "", SeverityLevel.Success) },
            { 8, new ResponseDescription("loami", "", SeverityLevel.Success) },
            { 9, new ResponseDescription("loki", "", SeverityLevel.Danger) },
            { 10, new ResponseDescription("adminReset", "reset administratora", SeverityLevel.Success) },
            { 11, new ResponseDescription("adminActiveChange", "", SeverityLevel.Success) },
            { 12, new ResponseDescription("adminOltConfiguration", "", SeverityLevel.Danger) },
            { 13, new ResponseDescription("adminSlotRestart", "", SeverityLevel.Danger) },
            { 14, new ResponseDescription("adminSlotRemove", "", SeverityLevel.Success) },
            { 15, new ResponseDescription("adminRogueOnuCandidate", "uszkodzony modem", SeverityLevel.Success) },
            { 16, new ResponseDescription("adminRogueOnu", "uszkodzony modem", SeverityLevel.Danger) },
            { 17, new ResponseDescription("adminRogueOnuSelfDetectBlock", "uszkodzony modem", SeverityLevel.Danger) },
            { 18, new ResponseDescription("adminTxOffOptic", "wyłączony laser", SeverityLevel.Success) },
            { 19, new ResponseDescription("adminDeactivate", "", SeverityLevel.Success) },
            { 20, new ResponseDescription("adminOltDeactivate", "", SeverityLevel.Danger) },
            { 21, new ResponseDescription("adminOmccDown", "", SeverityLevel.Success) },
            { 22, new ResponseDescription("adminSetRedundancy", "", SeverityLevel.Success) },
            { 23, new ResponseDescription("adminRemoveOnu", "", SeverityLevel.Danger) },
            { 100, new ResponseDescription("los", "", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unknown", "", SeverityLevel.Success) }
        };
    }
}