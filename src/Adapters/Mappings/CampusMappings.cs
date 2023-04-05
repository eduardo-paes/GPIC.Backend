using AutoMapper;
using Adapters.DTOs.Campus;
using Domain.Contracts.Campus;

namespace Adapters.Mappings
{
    public class CampusMappings : Profile
    {
        public CampusMappings()
        {
            CreateMap<CreateCampusInput, CreateCampusDTO>().ReverseMap();
            CreateMap<UpdateCampusInput, UpdateCampusDTO>().ReverseMap();
            CreateMap<ResumedReadCampusOutput, ResumedReadCampusDTO>().ReverseMap();
            CreateMap<DetailedReadCampusOutput, DetailedReadCampusDTO>().ReverseMap();
        }
    }
}