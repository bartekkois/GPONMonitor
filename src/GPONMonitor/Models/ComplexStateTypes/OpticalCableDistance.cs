namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalCableDistance
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
                    DescriptionEng = value.ToString() + " m";
                    DescriptionPol = value.ToString() + " m";
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
