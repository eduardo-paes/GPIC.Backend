using AutoMapper;
using Adapters.Gateways.Campus;
using Domain.Contracts.Campus;

namespace Adapters.Mappings
{
    public class CampusMappings : Profile
    {
        public CampusMappings()
        {
            CreateMap<CreateCampusInput, CreateCampusRequest>().ReverseMap();
            CreateMap<UpdateCampusInput, UpdateCampusRequest>().ReverseMap();
            CreateMap<ResumedReadCampusOutput, ResumedReadCampusResponse>().ReverseMap();
            CreateMap<DetailedReadCampusOutput, DetailedReadCampusResponse>().ReverseMap();
        }
    }
}