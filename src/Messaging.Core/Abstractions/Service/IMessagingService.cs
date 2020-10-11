using System.Threading.Tasks;
using Messaging.Core.Dto;
using Messaging.Core.Dto.Messaging;

namespace Messaging.Core.Abstractions.Service
{
    public interface IMessagingService
    {
        Task<BaseResponseDto> SendMessage(SendMessageRequestDto requestDto);

        Task<GetMessageListResponseDto> GetMessages(GetMessageListRequestDto requestDto);
    }
}
