using GPONMonitor.Services;

namespace GPONMonitor.Models.ComplexStateTypes
{
    public class OpticalConnectionDeactivationReason
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
                ResponseDescription responseDescription = _responseDescriptionDictionaries.OpticalConnectionDeactivationReasonResponse(value);
                Description = responseDescription.Description;
                Severity = responseDescription.Severity;
                this.value = value;
            }
        }

        public string Description { get; private set; }
        public SeverityLevel Severity { get; private set; }

        private readonly IResponseDescriptionDictionaries _responseDescriptionDictionaries;

        public OpticalConnectionDeactivationReason(IResponseDescriptionDictionaries responseDescriptionDictionaries)
        {
            _responseDescriptionDictionaries = responseDescriptionDictionaries;
        }
    }
}