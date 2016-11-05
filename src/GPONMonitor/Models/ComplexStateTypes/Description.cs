namespace GPONMonitor.Models.ComplexStateTypes
{
    public class Description
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
                    DescriptionEng = value.ToString().Replace("_", " ");
                    DescriptionPol = value.ToString().Replace("_", " ");
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
