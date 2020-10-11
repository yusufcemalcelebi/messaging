using System;
namespace Messaging.Core.Dto.Messaging
{
    public class SendMessageRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
    }
}
