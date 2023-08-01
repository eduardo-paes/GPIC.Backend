using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.SubArea;

namespace Domain.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            _ = CreateMap<SubArea, CreateSubAreaInput>().ReverseMap();
            _ = CreateMap<SubArea, UpdateSubAreaInput>().ReverseMap();
            _ = CreateMap<SubArea, ResumedReadSubAreaOutput>().ReverseMap();
            _ = CreateMap<SubArea, DetailedReadSubAreaOutput>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area!.MainArea, opt => opt.MapFrom(src => src.Area!.MainArea))
                .ReverseMap();
        }
    }
}