namespace Messaging.Core.Dto.Authentication
{
    public class RegisterResponseDto : BaseResponseDto
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
