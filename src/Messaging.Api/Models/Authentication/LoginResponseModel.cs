namespace Messaging.Api.Models.Authentication
{
    public class LoginResponseModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
