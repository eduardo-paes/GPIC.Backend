using AutoMapper;
using Domain.Contracts.Area;
using Domain.Entities;

namespace Domain.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            CreateMap<Area, CreateAreaInput>().ReverseMap();
            CreateMap<Area, UpdateAreaInput>().ReverseMap();
            CreateMap<Area, ResumedReadAreaOutput>().ReverseMap();
            CreateMap<Area, DetailedReadAreaOutput>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}