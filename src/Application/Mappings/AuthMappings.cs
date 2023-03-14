using System;
using AutoMapper;
using Application.DTOs.Auth;
using Domain.Entities;

namespace Application.Mappings
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

