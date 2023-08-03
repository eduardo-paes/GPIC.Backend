using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.AssistanceType;

namespace Domain.Mappings
{
    public class AssistanceTypeMappings : Profile
    {
        public AssistanceTypeMappings()
        {
            _ = CreateMap<AssistanceType, CreateAssistanceTypeInput>().ReverseMap();
            _ = CreateMap<AssistanceType, UpdateAssistanceTypeInput>().ReverseMap();
            _ = CreateMap<AssistanceType, ResumedReadAssistanceTypeOutput>().ReverseMap();
            _ = CreateMap<AssistanceType, DetailedReadAssistanceTypeOutput>().ReverseMap();
        }
    }
}