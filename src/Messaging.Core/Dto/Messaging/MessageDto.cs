using System;
namespace Messaging.Core.Dto.Messaging
{
    public class MessageDto
    {
        public string Text { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public DateTime Date { get; set; }
    }
}
