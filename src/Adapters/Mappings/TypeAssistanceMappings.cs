using Adapters.Gateways.AssistanceType;
using AutoMapper;
using Domain.UseCases.Ports.AssistanceType;

namespace Adapters.Mappings
{
    public class AssistanceTypeMappings : Profile
    {
        public AssistanceTypeMappings()
        {
            _ = CreateMap<CreateAssistanceTypeInput, CreateAssistanceTypeRequest>().ReverseMap();
            _ = CreateMap<UpdateAssistanceTypeInput, UpdateAssistanceTypeRequest>().ReverseMap();
            _ = CreateMap<ResumedReadAssistanceTypeOutput, ResumedReadAssistanceTypeResponse>().ReverseMap();
            _ = CreateMap<DetailedReadAssistanceTypeOutput, DetailedReadAssistanceTypeResponse>().ReverseMap();
        }
    }
}