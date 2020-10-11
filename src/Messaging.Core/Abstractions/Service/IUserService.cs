using System.Threading.Tasks;
using Messaging.Core.Dto.Authentication;

namespace Messaging.Core.Abstractions.Service
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);
        Task<bool> IsExists(int id);
    }
}
