using AutoMapper;
using Adapters.Gateways.MainArea;
using Domain.Contracts.MainArea;

namespace Adapters.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            CreateMap<CreateMainAreaInput, CreateMainAreaRequest>().ReverseMap();
            CreateMap<UpdateMainAreaInput, UpdateMainAreaRequest>().ReverseMap();
            CreateMap<ResumedReadMainAreaOutput, ResumedReadMainAreaResponse>().ReverseMap();
            CreateMap<DetailedMainAreaOutput, DetailedMainAreaResponse>().ReverseMap();
        }
    }
}