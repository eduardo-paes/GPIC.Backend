using AutoMapper;
using Domain.Contracts.SubArea;
using Domain.Entities;

namespace Domain.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            CreateMap<SubArea, CreateSubAreaInput>().ReverseMap();
            CreateMap<SubArea, UpdateSubAreaInput>().ReverseMap();
            CreateMap<SubArea, ResumedReadSubAreaOutput>().ReverseMap();
            CreateMap<SubArea, DetailedReadSubAreaOutput>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area!.MainArea, opt => opt.MapFrom(src => src.Area!.MainArea))
                .ReverseMap();
        }
    }
}