using System;
using System.Collections.Generic;
using Messaging.Core;

namespace Messaging.Api.Models.Messaging
{
    public class GetMessageListResponseModel : BasePaginationModel
    {
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }

        public List<MessageResponseModel> SentMessages { get; set; }
        public List<MessageResponseModel> ReceivedMessages { get; set; }
    }

    public class MessageResponseModel
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
