using AutoMapper;
using Adapters.DTOs.Area;
using Domain.Contracts.Area;

namespace Adapters.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            CreateMap<CreateAreaInput, CreateAreaDTO>().ReverseMap();
            CreateMap<UpdateAreaInput, UpdateAreaDTO>().ReverseMap();
            CreateMap<ResumedReadAreaOutput, ResumedReadAreaDTO>().ReverseMap();
            CreateMap<DetailedReadAreaOutput, DetailedReadAreaDTO>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}