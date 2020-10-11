using System.ComponentModel.DataAnnotations;

namespace Messaging.Api.Models.Authentication
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
