using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Helpers;
using Messaging.Api.Models.Messaging;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto.Authentication;
using Messaging.Core.Dto.Blocking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Messaging.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlockingController : ControllerBase
    {
        private readonly IBlockingService _blockingService;
        private readonly IMapper _mapper;
        private readonly ILogger<BlockingController> _logger;

        public BlockingController(IBlockingService blockingService, IMapper mapper,
            ILogger<BlockingController> logger)
        {
            _blockingService = blockingService;
            _mapper = mapper;
            _logger = logger;
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
                return BadRequest(new { response.ErrorMessages });

            _logger.LogInformation("Blocking request: Blocker:{0} - Blocked:{1} - IsActive:{2}",
                blockingDto.BlockerId, blockingDto.BlockedId, blockingDto.IsActive);

            return Ok();
        }
    }
}
