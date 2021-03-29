using System;

namespace Messaging.Data.Entities
{
    public class Message
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int FKSenderId { get; set; }
        public int FKReceiverId { get; set; }
        public bool IsSpam { get; set; }
        
        public DateTime Date { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }
    }
}
