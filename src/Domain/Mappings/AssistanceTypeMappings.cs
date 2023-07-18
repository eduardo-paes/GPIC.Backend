using AutoMapper;
using Domain.Contracts.AssistanceType;
using Domain.Entities;

namespace Domain.Mappings
{
    public class AssistanceTypeMappings : Profile
    {
        public AssistanceTypeMappings()
        {
            CreateMap<AssistanceType, CreateAssistanceTypeInput>().ReverseMap();
            CreateMap<AssistanceType, UpdateAssistanceTypeInput>().ReverseMap();
            CreateMap<AssistanceType, ResumedReadAssistanceTypeOutput>().ReverseMap();
            CreateMap<AssistanceType, DetailedReadAssistanceTypeOutput>().ReverseMap();
        }
    }
}