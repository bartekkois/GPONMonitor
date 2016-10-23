namespace GPONMonitor.Models
{
    public struct ResponseDescription
    {
        public string DescriptionEng { get; private set; }
        public string DescriptionPol { get; private set; }
        public SeverityLevel Severity { get; private set; }

        public ResponseDescription(string descriptionEng, string descriptionPol, SeverityLevel severity)
        {
            DescriptionEng = descriptionEng;
            DescriptionPol = descriptionPol;
            Severity = severity;
        }
    }
}
