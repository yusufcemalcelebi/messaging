using System;
using System.Threading.Tasks;
using Messaging.Core.Dto;
using Messaging.Core.Dto.Blocking;

namespace Messaging.Core.Abstractions.Service
{
    public interface IBlockingService
    {
        Task<bool> IsBlockExists(int senderId, int receiverId);

        Task<BaseResponseDto> InsertOrUpdateBlock(BlockingDto dto);
    }
}
