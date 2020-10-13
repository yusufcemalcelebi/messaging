using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Constants;
using Messaging.Core.Dto;
using Messaging.Core.Dto.Blocking;
using Messaging.Data;
using Messaging.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messaging.Service.Messaging
{
    public class BlockingService : IBlockingService
    {
        private readonly MessagingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BlockingService(MessagingDbContext dbContext, IMapper mapper,
            IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<BaseResponseDto> InsertOrUpdateBlock(BlockingDto dto)
        {
            var existedBlockEntity = await _dbContext.Blocks.FirstOrDefaultAsync(b =>
                b.FKBlockedUserId == dto.BlockedId &&
                b.FKBlockerUserId == dto.BlockerId);

            if (existedBlockEntity != null && existedBlockEntity.IsActive != dto.IsActive) {
                existedBlockEntity.IsActive = dto.IsActive;
            } else if (existedBlockEntity == null)
            {
                var isBlockedUserExists = await _userService.IsExists(dto.BlockedId);
                if (!isBlockedUserExists)
                    return new BaseResponseDto {
                        IsSuccess = false,
                        ErrorMessages = new HashSet<string> { ErrorMessages.BlockingBlockedUserNotFound }
                    };

                var newBlockEntity = _mapper.Map<Block>(dto);
                await _dbContext.Blocks.AddAsync(newBlockEntity);
            }

            await _dbContext.SaveChangesAsync();

            return new BaseResponseDto();
        }

        public async Task<bool> IsBlockExists(int senderId, int receiverId)
        {
            var isExists = await _dbContext.Blocks.AnyAsync(b => b.FKBlockedUserId == senderId &&
                                                                        b.FKBlockerUserId == receiverId);

            return isExists;
        }
    }
}
