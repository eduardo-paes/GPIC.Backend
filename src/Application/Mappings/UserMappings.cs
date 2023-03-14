using System;
using AutoMapper;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Mappings
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

