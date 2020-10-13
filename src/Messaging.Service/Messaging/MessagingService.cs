using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Constants;
using Messaging.Core.Dto;
using Messaging.Core.Dto.Messaging;
using Messaging.Data;
using Messaging.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messaging.Service.Messaging
{
    public class MessagingService : IMessagingService
    {
        private readonly IUserService _userService;
        private readonly MessagingDbContext _messagingDbContext;
        private readonly IBlockingService _blockService;
        private readonly IMapper _mapper;

        public MessagingService(IUserService userService, MessagingDbContext messagingDbContext,
            IBlockingService blockService, IMapper mapper)
        {
            _userService = userService;
            _messagingDbContext = messagingDbContext;
            _blockService = blockService;
            _mapper = mapper;
        }

        public async Task<GetMessageListResponseDto> GetMessages(GetMessageListRequestDto requestDto)
        {
            var messageEntities = await _messagingDbContext.Messages
                .Where(m => (m.FKReceiverId == requestDto.ReceiverId && m.FKSenderId == requestDto.SenderId) ||
                             (m.FKSenderId == requestDto.ReceiverId && m.FKReceiverId == requestDto.SenderId))
                .OrderByDescending(m => m.Date)
                .Skip((requestDto.Page - 1) * requestDto.Size)
                .Take(requestDto.Size)
                .ToListAsync();

            var messages = messageEntities.Select(m => _mapper.Map<MessageDto>(m)).ToList();

            return new GetMessageListResponseDto
            {
                Messages = messages
            };
        }

        public async Task<BaseResponseDto> SendMessage(SendMessageRequestDto requestDto)
        {
            var errorCodes = new HashSet<string>();

            var isReceiverExists = await _userService.IsExists(requestDto.ReceiverId);
            if (!isReceiverExists)
                errorCodes.Add(ErrorMessages.MessageReceiverNotFound);

            var anyBlock = await _blockService.IsBlockExists(requestDto.SenderId, requestDto.ReceiverId);
            if (anyBlock)
                errorCodes.Add(ErrorMessages.MessageBlockedUser);

            if (errorCodes.Any())
                return new BaseResponseDto
                {
                    ErrorMessages = errorCodes,
                    IsSuccess = false
                };


            var message = _mapper.Map<Message>(requestDto);
            message.Date = DateTime.Now;

            await _messagingDbContext.Messages.AddAsync(message);
            await _messagingDbContext.SaveChangesAsync();

            return new BaseResponseDto
            {
                IsSuccess = true
            };
        }

 
    }
}
