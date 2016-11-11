namespace GPONMonitor.Models.ComplexStateTypes
{
    public class DescriptionName
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
                    Description = value.ToString().Replace("_", " ");
                    Severity = SeverityLevel.Default;
                    this.value = value;
                }
                else
                {
                    Description = null;
                    Severity = SeverityLevel.Unknown;
                    this.value = null;
                }
            }
        }

        public string Description { get; private set; }
        public SeverityLevel Severity { get; private set; }
    }
}
