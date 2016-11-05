namespace GPONMonitor.Models.ComplexStateTypes
{
    public class GponSerialNumber
    {
        private string value;
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value != null)
                {
                    DescriptionEng = value.ToString();
                    DescriptionPol = value.ToString();
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
    }
}
