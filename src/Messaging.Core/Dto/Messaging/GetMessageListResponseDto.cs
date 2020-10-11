using System;
using System.Collections.Generic;

namespace Messaging.Core.Dto.Messaging
{
    public class GetMessageListResponseDto : BaseResponseDto
    {
        public IList<MessageDto> Messages { get; set; }
    }
}
