using AutoMapper;
using Adapters.Gateways.Area;
using Domain.Contracts.Area;

namespace Adapters.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            CreateMap<DetailedReadAreaOutput, DetailedReadAreaResponse>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}