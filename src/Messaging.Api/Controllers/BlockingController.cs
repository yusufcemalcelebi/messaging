using System;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Helpers;
using Messaging.Api.Models.Messaging;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto.Authentication;
using Messaging.Core.Dto.Blocking;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlockingController : ControllerBase
    {
        private readonly IBlockingService _blockingService;
        private readonly IMapper _mapper;

        public BlockingController(IBlockingService blockingService, IMapper mapper)
        {
            _blockingService = blockingService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(BlockingRequestModel requestModel)
        {
            var blockerUserDto = (UserDto)HttpContext.Items["User"];
            var blockingDto = _mapper.Map<BlockingDto>(requestModel);

            blockingDto.BlockerId = blockerUserDto.Id;

            var response = await _blockingService.InsertOrUpdateBlock(blockingDto);
            if (!response.IsSuccess)
                return BadRequest(new { response.ErrorCodes });

            return Ok();
        }
    }
}
