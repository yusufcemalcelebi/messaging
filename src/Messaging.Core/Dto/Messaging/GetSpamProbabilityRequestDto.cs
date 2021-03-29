namespace Messaging.Core.Dto.Messaging
{
    public class GetSpamProbabilityRequestDto
    {
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
