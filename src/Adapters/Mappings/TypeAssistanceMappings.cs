using AutoMapper;
using Adapters.Gateways.TypeAssistance;
using Domain.Contracts.TypeAssistance;

namespace Adapters.Mappings
{
    public class TypeAssistanceMappings : Profile
    {
        public TypeAssistanceMappings()
        {
            CreateMap<CreateTypeAssistanceInput, CreateTypeAssistanceRequest>().ReverseMap();
            CreateMap<UpdateTypeAssistanceInput, UpdateTypeAssistanceRequest>().ReverseMap();
            CreateMap<ResumedReadTypeAssistanceOutput, ResumedReadTypeAssistanceResponse>().ReverseMap();
            CreateMap<DetailedReadTypeAssistanceOutput, DetailedReadTypeAssistanceResponse>().ReverseMap();
        }
    }
}