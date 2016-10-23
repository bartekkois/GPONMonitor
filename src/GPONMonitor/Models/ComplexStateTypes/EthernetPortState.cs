using System.Collections.Generic;

namespace GPONMonitor.Models.Onu
{
    public class EthernetPortState
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

                if (EthernetPortStateResponseDictionary.ContainsKey(value))
                {
                    EthernetPortStateResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    EthernetPortStateResponseDictionary.TryGetValue(null, out responseDescription);
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


        // ONT Ethernet Port State
        // 1 - manual block
        // 2 - sourcemac block

        readonly Dictionary<int?, ResponseDescription> EthernetPortStateResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { null, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) },
            { 1, new ResponseDescription("manual block", "podniesiony", SeverityLevel.Danger) },
            { 2, new ResponseDescription("sourcemac block", "opuszczony", SeverityLevel.Success) },
        };
    }
}