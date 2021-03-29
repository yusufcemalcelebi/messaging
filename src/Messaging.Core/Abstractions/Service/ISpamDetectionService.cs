using System.Threading.Tasks;
using Messaging.Core.Dto;
using Messaging.Core.Dto.Messaging;

namespace Messaging.Core.Abstractions.Service
{
    public interface ISpamDetectionService
    {
        Task<bool> IsSpam(GetSpamProbabilityRequestDto requestDto);
    }
}
