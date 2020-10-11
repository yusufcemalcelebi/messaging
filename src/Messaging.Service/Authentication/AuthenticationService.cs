using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Models.Settings;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Constants;
using Messaging.Core.Dto.Authentication;
using Messaging.Data;
using Messaging.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Messaging.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOptions<AuthenticationSettings> _authenticationSettings;
        private readonly IMapper _mapper;
        private readonly MessagingDbContext _dbContext;

        public AuthenticationService(IOptions<AuthenticationSettings> authenticationSettings,
            IMapper mapper,
            MessagingDbContext dbContext)
        {
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto requestDto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x =>
                x.Username == requestDto.Username && x.Password == requestDto.Password);

            if (user == null)
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    ErrorCodes = new HashSet<string> { ErrorCodes.LoginInvalidLogin }
                };

            var token = GenerateJwtToken(user);

            var responseDto = _mapper.Map<LoginResponseDto>(user);
            responseDto.Token = token;
            responseDto.IsSuccess = true;

            return responseDto;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto requestDto)
        {
            var errorCodes = new HashSet<string>();

            var isEmailExists = await _dbContext.Users.AnyAsync(u => u.Email == requestDto.Email);
            if (isEmailExists)
                errorCodes.Add(ErrorCodes.RegisterEmailAlreadyExists);

            var isUsernameExists = await _dbContext.Users.AnyAsync(u => u.Username == requestDto.Username);
            if (isUsernameExists)
                errorCodes.Add(ErrorCodes.RegisterUsernameAlreadyExists);

            if (errorCodes.Any())
                return new RegisterResponseDto { IsSuccess = false, ErrorCodes = errorCodes };

            var userEntity = _mapper.Map<User>(requestDto);

            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            var token = GenerateJwtToken(userEntity);

            var responseDto = _mapper.Map<RegisterResponseDto>(userEntity);
            responseDto.IsSuccess = true;
            responseDto.Token = token;

            return responseDto;
        }

        private string GenerateJwtToken(User user)
        {

            var secretKey = Encoding.ASCII.GetBytes(_authenticationSettings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
