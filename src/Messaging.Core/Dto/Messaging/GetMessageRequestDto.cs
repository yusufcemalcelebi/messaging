using System;
namespace Messaging.Core.Dto.Messaging
{
    public class GetMessageListRequestDto : BasePaginationModel
    {
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
    }
}
