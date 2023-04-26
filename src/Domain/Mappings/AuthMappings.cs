using AutoMapper;
using Domain.Contracts.Auth;
using Domain.Entities;

namespace Domain.Mappings
{
    public class AuthMappings : Profile
    {
        public AuthMappings()
        {
            CreateMap<User, UserLoginOutput>().ReverseMap();
        }
    }
}