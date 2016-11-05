using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalConnectionState
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

                if (OpticalConnectionStateResponseDictionary.ContainsKey(value))
                {
                    OpticalConnectionStateResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    OpticalConnectionStateResponseDictionary.TryGetValue(null, out responseDescription);
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


        // ONU Link Status
        // 0 - invalid
        // 1 - inactive
        // 2 - active
        // 3 - running (OBSOLETE: removed in 5.08)

        readonly Dictionary<int?, ResponseDescription> OpticalConnectionStateResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { 0, new ResponseDescription("invalid", "niepoprawne", SeverityLevel.Danger) },
            { 1, new ResponseDescription("inactive", "nieaktywne", SeverityLevel.Danger) },
            { 2, new ResponseDescription("active", "aktywne", SeverityLevel.Success) },
            { 3, new ResponseDescription("running", "działające", SeverityLevel.Success) },
            { 255, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Success) }
        };
    }
}