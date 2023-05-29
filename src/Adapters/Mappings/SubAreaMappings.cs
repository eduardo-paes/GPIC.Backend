using AutoMapper;
using Adapters.Gateways.SubArea;
using Domain.Contracts.SubArea;

namespace Adapters.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            CreateMap<DetailedReadSubAreaOutput, DetailedReadSubAreaResponse>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area!.MainArea, opt => opt.MapFrom(src => src.Area!.MainArea))
                .ReverseMap();
        }
    }
}