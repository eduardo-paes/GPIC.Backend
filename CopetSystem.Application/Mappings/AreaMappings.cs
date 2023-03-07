using AutoMapper;
using CopetSystem.Application.DTOs.Area;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Application.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            CreateMap<Area, CreateAreaDTO>().ReverseMap();
            CreateMap<Area, UpdateAreaDTO>().ReverseMap();
            CreateMap<Area, ReadAreaDTO>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}

