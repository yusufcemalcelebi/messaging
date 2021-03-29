namespace Messaging.Api.Models.Settings
{
    public class SpamDetectionSettings
    {
        public string Url { get; set; }
        public float SpamProbabilityThreshold { get; set; }
    }
}
