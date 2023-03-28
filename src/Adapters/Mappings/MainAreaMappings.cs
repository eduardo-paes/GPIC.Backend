using AutoMapper;
using Adapters.DTOs.MainArea;
using Domain.Contracts.MainArea;

namespace Adapters.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            CreateMap<CreateMainAreaInput, CreateMainAreaDTO>().ReverseMap();
            CreateMap<UpdateMainAreaInput, UpdateMainAreaDTO>().ReverseMap();
            CreateMap<ResumedReadMainAreaOutput, ResumedReadMainAreaDTO>().ReverseMap();
            CreateMap<DetailedMainAreaOutput, DetailedMainAreaDTO>().ReverseMap();
        }
    }
}