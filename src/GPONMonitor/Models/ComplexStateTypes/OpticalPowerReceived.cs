namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalPowerReceived
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
                decimal parsedValue;

                if (decimal.TryParse(value, out parsedValue))
                {
                    decimal calculateddBmPower = parsedValue / 10;

                    if (calculateddBmPower < -9.0m && calculateddBmPower > -26.0m)
                    {
                        Severity = SeverityLevel.Success;
                    }
                    else if ((calculateddBmPower < -8.0m && calculateddBmPower >= -9.0m) || (calculateddBmPower <= -26.0m && calculateddBmPower > -27.0m))
                    {
                        Severity = SeverityLevel.Warning;
                    }
                    else
                    {
                        Severity = SeverityLevel.Danger;
                    }

                    Description = calculateddBmPower + " dBm";
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


        // ONU Optical Power Received
        // -400 - no signal (-40,0 dBm)
        // other value - dBm level
    }
}