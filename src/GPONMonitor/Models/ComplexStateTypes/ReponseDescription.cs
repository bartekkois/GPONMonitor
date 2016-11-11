namespace GPONMonitor.Models.ComplexStateTypes
{
    public struct ResponseDescription
    {
        public string Description { get; private set; }
        public SeverityLevel Severity { get; private set; }

        public ResponseDescription(string description, SeverityLevel severity)
        {
            Description = description;
            Severity = severity;
        }
    }
}
