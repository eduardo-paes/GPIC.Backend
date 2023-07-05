using AutoMapper;
using Domain.Contracts.TypeAssistance;
using Domain.Entities;

namespace Domain.Mappings
{
    public class TypeAssistanceMappings : Profile
    {
        public TypeAssistanceMappings()
        {
            CreateMap<TypeAssistance, CreateTypeAssistanceInput>().ReverseMap();
            CreateMap<TypeAssistance, UpdateTypeAssistanceInput>().ReverseMap();
            CreateMap<TypeAssistance, ResumedReadTypeAssistanceOutput>().ReverseMap();
            CreateMap<TypeAssistance, DetailedReadTypeAssistanceOutput>().ReverseMap();
        }
    }
}