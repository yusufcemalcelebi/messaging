using System;
using System.Collections.Generic;

namespace Messaging.Core.Dto
{
    public class BaseResponseDto
    {
        public BaseResponseDto()
        {
            ErrorMessages = new HashSet<string>();
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public HashSet<string> ErrorMessages { get; set; }
    }
}
