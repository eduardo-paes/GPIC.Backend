using Adapters.Gateways.Area;
using AutoMapper;
using Domain.UseCases.Ports.Area;

namespace Adapters.Mappings
{
    public class AreaMappings : Profile
    {
        public AreaMappings()
        {
            _ = CreateMap<CreateAreaInput, CreateAreaRequest>().ReverseMap();
            _ = CreateMap<UpdateAreaInput, UpdateAreaRequest>().ReverseMap();
            _ = CreateMap<ResumedReadAreaOutput, ResumedReadAreaResponse>().ReverseMap();
            _ = CreateMap<DetailedReadAreaOutput, DetailedReadAreaResponse>()
                .ForMember(dest => dest.MainArea, opt => opt.MapFrom(src => src.MainArea))
                .ReverseMap();
        }
    }
}