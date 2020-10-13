using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Helpers;
using Messaging.Api.Models.Messaging;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto.Authentication;
using Messaging.Core.Dto.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessagingController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        private readonly IMapper _mapper;

        public MessagingController(IMessagingService messagingService, IMapper mapper)
        {
            _messagingService = messagingService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync(SendMessageRequestModel requestModel)
        {
            var senderUserDto = (UserDto) HttpContext.Items["User"];
            var requestDto = _mapper.Map<SendMessageRequestDto>(requestModel);

            requestDto.SenderId = senderUserDto.Id;

            var responseDto = await _messagingService.SendMessage(requestDto);
            if (!responseDto.IsSuccess)
                return BadRequest(new { responseDto.ErrorMessages });

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]GetMessageListRequestModel requestModel)
        {
            var userDto = (UserDto)HttpContext.Items["User"];
            var requestDto = _mapper.Map<GetMessageListRequestDto>(requestModel);

            requestDto.SenderId = userDto.Id;

            var responseDto = await _messagingService.GetMessages(requestDto);
            var response = new GetMessageListResponseModel
            {
                ReceiverId = requestDto.ReceiverId,
                SenderId = requestDto.SenderId,
                Page = requestDto.Page,
                Size = responseDto.Messages.Count(),
                SentMessages = responseDto.Messages.Where(m => m.SenderId == requestDto.SenderId)
                    .Select(m => _mapper.Map<MessageResponseModel>(m))
                    .ToList(),
                ReceivedMessages = responseDto.Messages.Where(m => m.ReceiverId == requestDto.SenderId)
                    .Select(m => _mapper.Map<MessageResponseModel>(m))
                    .ToList()
            };

            return Ok(new { response });
        }
    }
}
