namespace Messaging.Data.Entities
{
    public class Block
    {
        public int ID { get; set; }
        public int FKBlockerUserId { get; set; }
        public int FKBlockedUserId { get; set; }
        public bool IsActive { get; set; }

        public User BlockerUser { get; set; }

        public User BlockedUser { get; set; }
    }
}
