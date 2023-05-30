using AutoMapper;
using Adapters.Gateways.SubArea;
using Domain.Contracts.SubArea;

namespace Adapters.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            CreateMap<CreateSubAreaInput, CreateSubAreaRequest>().ReverseMap();
            CreateMap<UpdateSubAreaInput, UpdateSubAreaRequest>().ReverseMap();
            CreateMap<ResumedReadSubAreaOutput, ResumedReadSubAreaResponse>().ReverseMap();
            CreateMap<DetailedReadSubAreaOutput, DetailedReadSubAreaResponse>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area!.MainArea, opt => opt.MapFrom(src => src.Area!.MainArea))
                .ReverseMap();
        }
    }
}