using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Models.Authentication;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel)
        {
            var requestDto = _mapper.Map<LoginRequestDto>(requestModel);
            var responseDto = await _authenticationService.LoginAsync(requestDto);

            if (!responseDto.IsSuccess)
                return BadRequest(new { responseDto.ErrorMessages });

            var responseModel = _mapper.Map<LoginResponseModel>(responseDto);
            return Ok(responseModel);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel requestModel)
        {
            var requestDto = _mapper.Map<RegisterRequestDto>(requestModel);
            var responseDto = await _authenticationService.RegisterAsync(requestDto);

            if (!responseDto.IsSuccess)
                return BadRequest(new { responseDto.ErrorMessages });

            var responseModel = _mapper.Map<RegisterResponseModel>(responseDto);
            return Ok(responseModel);
        }
    }
}
