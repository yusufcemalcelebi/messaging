using System;
namespace Messaging.Api.Models.Messaging
{
    public class SendMessageRequestModel
    {
        public int ReceiverId { get; set; }
        public string Text { get; set; }
    }
}
