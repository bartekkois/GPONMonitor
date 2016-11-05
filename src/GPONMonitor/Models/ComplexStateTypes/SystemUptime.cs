using System;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class SystemUptime
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
    }
}