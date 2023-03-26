using AutoMapper;
using Adapters.DTOs.SubArea;
using Domain.Entities;

namespace Adapters.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            CreateMap<SubArea, CreateSubAreaDTO>().ReverseMap();
            CreateMap<SubArea, UpdateSubAreaDTO>().ReverseMap();
            CreateMap<SubArea, ResumedReadSubAreaDTO>().ReverseMap();
            CreateMap<SubArea, DetailedReadSubAreaDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area.MainArea, opt => opt.MapFrom(src => src.Area.MainArea))
                .ReverseMap();
        }
    }
}