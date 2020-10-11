using System;
using System.Collections.Generic;

namespace Messaging.Core.Dto
{
    public class BaseResponseDto
    {
        public BaseResponseDto()
        {
            ErrorCodes = new HashSet<string>();
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public HashSet<string> ErrorCodes { get; set; }
    }
}
