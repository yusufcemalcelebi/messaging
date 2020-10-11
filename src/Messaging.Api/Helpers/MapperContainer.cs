using AutoMapper;
using Messaging.Api.Models.Authentication;
using Messaging.Core.Dto.Authentication;
using Messaging.Data.Entities;

namespace Messaging.Api.Helpers
{
    public class MapperContainer : Profile
    {
        public MapperContainer()
        {
            #region Authentication

            CreateMap<LoginRequestModel, LoginRequestDto>();
            CreateMap<LoginResponseDto, LoginResponseModel>();

            CreateMap<UserDto, LoginResponseDto>();

            CreateMap<RegisterRequestModel, RegisterRequestDto>();
            CreateMap<RegisterRequestDto, User>();
            CreateMap<RegisterResponseDto, RegisterResponseModel>();

            CreateMap<User, RegisterResponseDto>();
            CreateMap<User, LoginResponseDto>();
            CreateMap<User, UserDto>();

            #endregion
        }
    }
}
