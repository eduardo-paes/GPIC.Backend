using System;
using AutoMapper;
using Adapters.DTOs.User;
using Domain.Entities;

namespace Adapters.Mappings
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

