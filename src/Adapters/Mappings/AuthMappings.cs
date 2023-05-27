using AutoMapper;
using Adapters.Gateways.Auth;
using Domain.Contracts.Auth;

namespace Adapters.Mappings
{
    public class AuthMappings : Profile
    {
        public AuthMappings()
        {
            CreateMap<UserLoginInput, UserLoginRequest>().ReverseMap();
            CreateMap<UserLoginOutput, UserLoginResponse>().ReverseMap();
            CreateMap<UserResetPasswordInput, UserResetPasswordRequest>().ReverseMap();
        }
    }
}