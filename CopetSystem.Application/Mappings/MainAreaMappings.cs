using System;
using AutoMapper;
using CopetSystem.Application.DTOs.MainArea;
using CopetSystem.Domain.Entities;

namespace CopetSystem.Application.Mappings
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

