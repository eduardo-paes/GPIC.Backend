using System;
using AutoMapper;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Application.Mappings
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

