using AutoMapper;
using Domain.Contracts.MainArea;
using Domain.Entities;

namespace Domain.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            CreateMap<MainArea, CreateMainAreaInput>().ReverseMap();
            CreateMap<MainArea, UpdateMainAreaInput>().ReverseMap();
            CreateMap<MainArea, ResumedReadMainAreaOutput>().ReverseMap();
            CreateMap<MainArea, DetailedMainAreaOutput>().ReverseMap();
        }
    }
}