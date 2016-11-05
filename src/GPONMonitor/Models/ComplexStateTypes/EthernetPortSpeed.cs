using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class EthernetPortSpeed
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
                ResponseDescription responseDescription;

                if (EthernetPortSpeedResponseDictionary.ContainsKey(value))
                {
                    EthernetPortSpeedResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    EthernetPortSpeedResponseDictionary.TryGetValue(null, out responseDescription);
                }

                DescriptionEng = responseDescription.DescriptionEng;
                DescriptionPol = responseDescription.DescriptionPol;
                Severity = responseDescription.Severity;
                this.value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }


        // ONT Ethernet Port Speed
        // 1 - 10 Mb/s
        // 2 - 100 Mb/s
        // 3 - 1000 Mb/s

        readonly Dictionary<int?, ResponseDescription> EthernetPortSpeedResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 1, new ResponseDescription("10 Mb/s", "10 Mb/s", SeverityLevel.Success) },
            { 2, new ResponseDescription("100 Mb/s", "100 Mb/s", SeverityLevel.Success) },
            { 3, new ResponseDescription("1000 Mb/s", "1000 Mb/s", SeverityLevel.Success) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) }
        };
    }
}