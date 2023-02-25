using AutoMapper;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.MainArea;
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
            CreateMap<User, UserLoginResponseDTO>().ReverseMap();

            // MainArea
            CreateMap<MainArea, CreateMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, UpdateMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, ReadMainAreaDTO>().ReverseMap();
        }
    }
}

