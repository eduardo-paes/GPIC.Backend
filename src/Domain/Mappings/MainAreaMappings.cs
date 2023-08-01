using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.MainArea;

namespace Domain.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            _ = CreateMap<MainArea, CreateMainAreaInput>().ReverseMap();
            _ = CreateMap<MainArea, UpdateMainAreaInput>().ReverseMap();
            _ = CreateMap<MainArea, ResumedReadMainAreaOutput>().ReverseMap();
            _ = CreateMap<MainArea, DetailedMainAreaOutput>().ReverseMap();
        }
    }
}