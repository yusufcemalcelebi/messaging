using System.Collections.Generic;

namespace Messaging.Data.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Block> BlockedUserList { get; set; }
        public ICollection<Block> BlockerUserList { get; set; }

        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}
