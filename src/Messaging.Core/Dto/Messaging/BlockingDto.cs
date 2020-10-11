using System;
namespace Messaging.Core.Dto.Blocking
{
    public class BlockingDto
    {
        public int BlockedId { get; set; }
        public int BlockerId { get; set; }

        public bool IsActive { get; set; }
    }
}
