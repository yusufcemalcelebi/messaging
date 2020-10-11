using System;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto.Authentication;
using Messaging.Data;
using Microsoft.EntityFrameworkCore;

namespace Messaging.Service
{
    public class UserService : IUserService
    {
        private readonly MessagingDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(MessagingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> GetById(int id)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.ID == id);
            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task<bool> IsExists(int id)
        {
            var isExists  = await _dbContext.Users.AnyAsync(u => u.ID == id);
            return isExists;
        }
    }
}
