using Adapters.Gateways.Campus;
using AutoMapper;
using Domain.UseCases.Ports.Campus;

namespace Adapters.Mappings
{
    public class CampusMappings : Profile
    {
        public CampusMappings()
        {
            _ = CreateMap<CreateCampusInput, CreateCampusRequest>().ReverseMap();
            _ = CreateMap<UpdateCampusInput, UpdateCampusRequest>().ReverseMap();
            _ = CreateMap<ResumedReadCampusOutput, ResumedReadCampusResponse>().ReverseMap();
            _ = CreateMap<DetailedReadCampusOutput, DetailedReadCampusResponse>().ReverseMap();
        }
    }
}