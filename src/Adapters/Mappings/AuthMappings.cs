using Adapters.Gateways.Auth;
using AutoMapper;
using Domain.UseCases.Ports.Auth;

namespace Adapters.Mappings
{
    public class AuthMappings : Profile
    {
        public AuthMappings()
        {
            _ = CreateMap<UserLoginInput, UserLoginRequest>().ReverseMap();
            _ = CreateMap<UserLoginOutput, UserLoginResponse>().ReverseMap();
            _ = CreateMap<UserResetPasswordInput, UserResetPasswordRequest>().ReverseMap();
        }
    }
}