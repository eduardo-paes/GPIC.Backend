using System;
using AutoMapper;
using Application.DTOs.MainArea;
using Domain.Entities;

namespace Application.Mappings
{
    public class MainAreaMappings : Profile
    {
        public MainAreaMappings()
        {
            CreateMap<MainArea, CreateMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, UpdateMainAreaDTO>().ReverseMap();
            CreateMap<MainArea, ReadMainAreaDTO>().ReverseMap();
        }
    }
}

