using Adapters.Gateways.MainArea;
using AutoMapper;
using Domain.UseCases.Ports.MainArea;

namespace Adapters.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            _ = CreateMap<CreateMainAreaInput, CreateMainAreaRequest>().ReverseMap();
            _ = CreateMap<UpdateMainAreaInput, UpdateMainAreaRequest>().ReverseMap();
            _ = CreateMap<ResumedReadMainAreaOutput, ResumedReadMainAreaResponse>().ReverseMap();
            _ = CreateMap<DetailedMainAreaOutput, DetailedReadMainAreaResponse>().ReverseMap();
        }
    }
}