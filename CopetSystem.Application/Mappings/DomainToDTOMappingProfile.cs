using AutoMapper;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            // User
            CreateMap<User, UserReadDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

            // Auth
            CreateMap<User, UserRegisterDTO>().ReverseMap();
        }
    }
}

