using System;
namespace Messaging.Api.Models.Messaging
{
    public class BlockingRequestModel
    {
        public int BlockedId { get; set; }
        public bool IsActive { get; set; }
    }
}
