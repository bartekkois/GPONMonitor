using Newtonsoft.Json;
using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class EthernetPortState
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
                this.value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.6.1.1.1.5";           // ETH port status (followed by OnuPortId, OnuId, 1 and PortNumber)


        // ONT Ethernet Port State
        // 1 - manual block
        // 2 - sourcemac block

        readonly Dictionary<int?, ResponseDescription> EthernetPortStateResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 1, new ResponseDescription("up", "podniesiony", SeverityLevel.Danger) },
            { 2, new ResponseDescription("down", "opuszczony", SeverityLevel.Success) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) }
        };
    }
}