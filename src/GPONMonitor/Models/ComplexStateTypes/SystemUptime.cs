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

                    Description = displayTimeSpan;
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