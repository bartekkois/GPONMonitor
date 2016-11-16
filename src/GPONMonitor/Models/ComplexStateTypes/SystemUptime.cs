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
                    Description = TimeSpan.FromSeconds(Convert.ToDouble(value)).ToString(@"d\d\ hh\:mm\:ss");
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