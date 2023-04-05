using AutoMapper;
using Domain.Contracts.Campus;
using Domain.Entities;

namespace Domain.Mappings
{
    public class CampusMappings : Profile
    {
        public CampusMappings()
        {
            CreateMap<Campus, CreateCampusInput>().ReverseMap();
            CreateMap<Campus, UpdateCampusInput>().ReverseMap();
            CreateMap<Campus, ResumedReadCampusOutput>().ReverseMap();
            CreateMap<Campus, DetailedReadCampusOutput>().ReverseMap();
        }
    }
}