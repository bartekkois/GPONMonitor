using Newtonsoft.Json;
using System;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalConnectionInactiveTime
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
                if (value != null)
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(Convert.ToDouble(value));
                    string displayTimeSpan = timeSpan.ToString("dd, hh:mm:tt");

                    DescriptionEng = displayTimeSpan;
                    DescriptionPol = displayTimeSpan;
                    Severity = SeverityLevel.Default;
                    this.value = value;
                }
                else
                {
                    DescriptionEng = null;
                    DescriptionPol = null;
                    Severity = SeverityLevel.Unknown;
                    this.value = null;
                }
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.1.1.85";               // ONU optical connections inactive time (the elapsed time after ont Inactive) (followed by OnuPortId and OnuId)
        [JsonIgnore]
        public string SnmpOIDOnuTimeUpdate1 { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.2.1";    // ONU time update (set 21)
        [JsonIgnore]
        public string SnmpOIDOnuTimeUpdate2 { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.2.6";    // ONU time update (set OltPortId)
        [JsonIgnore]
        public string SnmpOIDOnuTimeUpdate3 { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.2.7";    // ONU time update (set OnuId)
        [JsonIgnore]
        public string SnmpOIDOnuTimeUpdate4 { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.2.3";    // ONU time update (set 0)
    }
}
