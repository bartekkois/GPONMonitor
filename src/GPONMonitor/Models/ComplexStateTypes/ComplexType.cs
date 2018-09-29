namespace GPONMonitor.Models.ComplexStateTypes
{
    public abstract class ComplexType
    {
        public string Description { get; private set; }
        public SeverityLevel Severity { get; private set; }

        public ComplexType( string description, SeverityLevel severity)
        {
            Description = description;
            Severity = severity;
        }

        public ComplexType(ResponseDescription responseDescription)
        {
            Description = responseDescription.Description;
            Severity = responseDescription.Severity;
        }
    }
}
