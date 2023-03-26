using AutoMapper;
using Adapters.DTOs.MainArea;
using Domain.Entities;

namespace Adapters.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            CreateMap<MainArea, CreateMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, UpdateMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, ResumedReadMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, DetailedMainAreaDTO>().ReverseMap();
        }
    }
}