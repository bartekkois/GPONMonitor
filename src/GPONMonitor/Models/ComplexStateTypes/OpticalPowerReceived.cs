namespace GPONMonitor.Models.Onu
{
    public class OpticalPowerReceived
    {
        public string Value
        {
            get
            {
                return Value;
            }
            set
            {
                int parsedValue;

                if (int.TryParse(value, out parsedValue))
                {
                    decimal calculateddBmPower = parsedValue/10;

                    if (calculateddBmPower < -14.0m && calculateddBmPower > -26.0m)
                    {
                        Severity = SeverityLevel.Success;
                    }
                    else if((calculateddBmPower < -13.0m && calculateddBmPower >= -14.0m) || (calculateddBmPower <= -26.0m && calculateddBmPower > -27.0m))
                    {
                        Severity = SeverityLevel.Warning;
                    }
                    else
                    {
                        Severity = SeverityLevel.Danger;
                    }

                    Value = calculateddBmPower + " dBm";
                }
                else
                {
                    DescriptionEng = "unknown";
                    DescriptionPol = "brak odczytu";
                    Severity = SeverityLevel.Unknown;

                    Value = null;
                }
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }


        // ONU Optical Power Received
        // -400 - no signal (-40,0 dBm)
        // other value - dBm level
    }
}