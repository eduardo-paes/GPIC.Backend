using AutoMapper;
using Adapters.DTOs.SubArea;
using Domain.Contracts.SubArea;

namespace Adapters.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            CreateMap<CreateSubAreaInput, CreateSubAreaDTO>().ReverseMap();
            CreateMap<UpdateSubAreaInput, UpdateSubAreaDTO>().ReverseMap();
            CreateMap<ResumedReadSubAreaOutput, ResumedReadSubAreaDTO>().ReverseMap();
            CreateMap<DetailedReadSubAreaOutput, DetailedReadSubAreaDTO>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area!.MainArea, opt => opt.MapFrom(src => src.Area!.MainArea))
                .ReverseMap();
        }
    }
}