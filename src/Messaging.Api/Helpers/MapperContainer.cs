using AutoMapper;
using Messaging.Api.Models.Authentication;
using Messaging.Api.Models.Messaging;
using Messaging.Core.Dto.Authentication;
using Messaging.Core.Dto.Blocking;
using Messaging.Core.Dto.Messaging;
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

            CreateMap<SendMessageRequestModel, SendMessageRequestDto>();

            CreateMap<SendMessageRequestDto, Message>()
            .ForMember(dest =>
                dest.FKReceiverId,
                opt => opt.MapFrom(src => src.ReceiverId))
            .ForMember(dest =>
                dest.FKSenderId,
                opt => opt.MapFrom(src => src.SenderId));

            CreateMap<GetMessageListRequestModel, GetMessageListRequestDto>();

            CreateMap<Message, MessageDto>()
                .ForMember(dest =>
                    dest.ReceiverId,
                    opt => opt.MapFrom(src => src.FKReceiverId))
                .ForMember(dest =>
                    dest.SenderId,
                    opt => opt.MapFrom(src => src.FKSenderId));

            CreateMap<MessageDto, MessageResponseModel>();

            CreateMap<BlockingRequestModel, BlockingDto>();

            CreateMap<BlockingDto, Block>()
                .ForMember(dest =>
                    dest.FKBlockedUserId,
                    opt => opt.MapFrom(src => src.BlockedId))
                .ForMember(dest =>
                    dest.FKBlockerUserId,
                    opt => opt.MapFrom(src => src.BlockerId));

            #endregion
        }
    }
}
