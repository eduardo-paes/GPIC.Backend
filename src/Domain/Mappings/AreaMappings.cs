using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.Area;

namespace Domain.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            _ = CreateMap<Area, CreateAreaInput>().ReverseMap();
            _ = CreateMap<Area, UpdateAreaInput>().ReverseMap();
            _ = CreateMap<Area, ResumedReadAreaOutput>().ReverseMap();
            _ = CreateMap<Area, DetailedReadAreaOutput>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}