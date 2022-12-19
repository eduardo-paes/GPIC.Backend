using AutoMapper;
using CopetSystem.Application.DTOs;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserReadDTO>().ReverseMap();
        }
    }
}

