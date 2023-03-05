using System;
using AutoMapper;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Application.Mappings
{
	public class UserMappings : Profile
	{
		public UserMappings()
		{
            CreateMap<User, UserReadDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
	}
}

