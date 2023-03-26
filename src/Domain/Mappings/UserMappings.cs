using System;
using AutoMapper;
using Domain.Contracts.User;
using Domain.Entities;

namespace Domain.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserReadOutput>().ReverseMap();
            CreateMap<User, UserUpdateInput>().ReverseMap();
        }
    }
}

