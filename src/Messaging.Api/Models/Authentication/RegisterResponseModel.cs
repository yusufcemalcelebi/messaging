using System;
namespace Messaging.Api.Models.Authentication
{
    public class RegisterResponseModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
