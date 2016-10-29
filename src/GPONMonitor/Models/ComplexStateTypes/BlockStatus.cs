using Newtonsoft.Json;
using System.Collections.Generic;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class BlockStatus
    {
        public int? Value
        {
            get
            {
                return Value;
            }
            set
            {
                ResponseDescription responseDescription;

                if (BlockStatusResponseDictionary.ContainsKey(value))
                {
                    BlockStatusResponseDictionary.TryGetValue(value, out responseDescription);
                }
                else
                {
                    BlockStatusResponseDictionary.TryGetValue(null, out responseDescription);
                }

                DescriptionEng = responseDescription.DescriptionEng;
                DescriptionPol = responseDescription.DescriptionPol;
                Severity = responseDescription.Severity;

                Value = value;
            }
        }

        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        [JsonIgnore]
        public string SnmpOID { get; private set; } = "1.3.6.1.4.1.6296.101.23.3.1.1.55";                      // Block status (followed by OnuPortId and OnuId)


        // ONT Block Status
        // 1 - autoblock
        // 2 - manual block
        // 255 - unblock

        readonly Dictionary<int?, ResponseDescription> BlockStatusResponseDictionary = new Dictionary<int?, ResponseDescription>()
        {
            { null, new ResponseDescription("unknown", "brak odczytu", SeverityLevel.Unknown) },
            { 1, new ResponseDescription("autoblock", "blokada automatyczna", SeverityLevel.Danger) },
            { 2, new ResponseDescription("manual block", "blokada ręczna", SeverityLevel.Danger) },
            { 255, new ResponseDescription("unblock", "brak blokady", SeverityLevel.Success) },
        };
    }
}