using AutoMapper;
using Adapters.DTOs.Auth;
using Domain.Contracts.Auth;

namespace Adapters.Mappings
{
    public class AuthMappings : Profile
    {
        public AuthMappings()
        {
            CreateMap<UserLoginInput, UserLoginRequestDTO>().ReverseMap();
            CreateMap<UserLoginOutput, UserLoginResponseDTO>().ReverseMap();
            CreateMap<UserResetPasswordInput, UserResetPasswordDTO>().ReverseMap();
        }
    }
}