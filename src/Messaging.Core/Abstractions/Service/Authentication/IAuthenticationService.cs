using System.Threading.Tasks;
using Messaging.Core.Dto.Authentication;

namespace Messaging.Core.Abstractions.Service
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto requestDto);

        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto requestDto);
    }
}
