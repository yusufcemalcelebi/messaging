using System;
using Messaging.Core;

namespace Messaging.Api.Models.Messaging
{
    public class GetMessageListRequestModel : BasePaginationModel
    {
        public int ReceiverId { get; set; }
    }
}
