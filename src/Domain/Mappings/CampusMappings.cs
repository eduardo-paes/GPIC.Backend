using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.Campus;

namespace Domain.Mappings
{
    public class CampusMappings : Profile
    {
        public CampusMappings()
        {
            _ = CreateMap<Campus, CreateCampusInput>().ReverseMap();
            _ = CreateMap<Campus, UpdateCampusInput>().ReverseMap();
            _ = CreateMap<Campus, ResumedReadCampusOutput>().ReverseMap();
            _ = CreateMap<Campus, DetailedReadCampusOutput>().ReverseMap();
        }
    }
}