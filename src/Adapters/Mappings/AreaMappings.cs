using AutoMapper;
using Adapters.DTOs.Area;
using Domain.Entities;

namespace Adapters.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            CreateMap<Area, CreateAreaDTO>().ReverseMap();
            CreateMap<Area, UpdateAreaDTO>().ReverseMap();
            CreateMap<Area, ResumedReadAreaDTO>().ReverseMap();
            CreateMap<Area, DetailedReadAreaDTO>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}