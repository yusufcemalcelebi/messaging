using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Models.Authentication;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Messaging.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper,
            ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel)
        {
            var requestDto = _mapper.Map<LoginRequestDto>(requestModel);
            var responseDto = await _authenticationService.LoginAsync(requestDto);

            if (!responseDto.IsSuccess)
            {
                _logger.LogInformation("Invalid login. Username:{0}", requestModel.Username);
                return BadRequest(new { responseDto.ErrorMessages });
            }

            var responseModel = _mapper.Map<LoginResponseModel>(responseDto);

            _logger.LogInformation("User:{0} successfully logged in.", responseDto.ID);

            return Ok(responseModel);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel requestModel)
        {
            var requestDto = _mapper.Map<RegisterRequestDto>(requestModel);
            var responseDto = await _authenticationService.RegisterAsync(requestDto);

            if (!responseDto.IsSuccess)
            {
                _logger.LogInformation("Invalid registration request. Username: {0}", requestModel.Username);
                return BadRequest(new { responseDto.ErrorMessages });
            }

            var responseModel = _mapper.Map<RegisterResponseModel>(responseDto);

            _logger.LogInformation("User:{0} is registered successfully.", responseDto.ID);

            return Ok(responseModel);
        }
    }
}
