using System;
using AutoMapper;
using Adapters.DTOs.Auth;
using Domain.Entities;

namespace Adapters.Mappings
{
    public class AuthMappings : Profile
    {
        public AuthMappings()
        {
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserLoginResponseDTO>().ReverseMap();
        }
    }
}