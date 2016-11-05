﻿using Newtonsoft.Json;

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

                    if (calculateddBmPower < -14.0m && calculateddBmPower > -26.0m)
                    {
                        Severity = SeverityLevel.Success;
                    }
                    else if ((calculateddBmPower < -13.0m && calculateddBmPower >= -14.0m) || (calculateddBmPower <= -26.0m && calculateddBmPower > -27.0m))
                    {
                        Severity = SeverityLevel.Warning;
                    }
                    else
                    {
                        Severity = SeverityLevel.Danger;
                    }

                    DescriptionEng = calculateddBmPower + " dBm";
                    DescriptionPol = calculateddBmPower + " dBm";
                    this.value = value;
                }
                else
                {
                    DescriptionEng = "unknown";
                    DescriptionPol = "brak odczytu";
                    Severity = SeverityLevel.Unknown;
                    this.value = null;
                }
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.1.1.16";             // Optical power received (followed by OnuPortId and OnuId)


        // ONU Optical Power Received
        // -400 - no signal (-40,0 dBm)
        // other value - dBm level
    }
}