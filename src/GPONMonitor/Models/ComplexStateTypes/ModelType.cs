using Newtonsoft.Json;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class ModelType
    {
        public string Value
        {
            get
            {
                return Value;
            }
            set
            {
                if (value != null)
                {
                    DescriptionEng = value.ToString();
                    DescriptionPol = value.ToString();
                    Severity = SeverityLevel.Default;

                    Value = value;
                }
                else
                {
                    DescriptionEng = null;
                    DescriptionPol = null;
                    Severity = SeverityLevel.Unknown;

                    Value = null;
                }
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.1.1.17";                        // Model type (followed by OnuPortId and OnuId)
    }
}
