using AutoMapper;
using Adapters.Gateways.AssistanceType;
using Domain.Contracts.AssistanceType;

namespace Adapters.Mappings
{
    public class AssistanceTypeMappings : Profile
    {
        public AssistanceTypeMappings()
        {
            CreateMap<CreateAssistanceTypeInput, CreateAssistanceTypeRequest>().ReverseMap();
            CreateMap<UpdateAssistanceTypeInput, UpdateAssistanceTypeRequest>().ReverseMap();
            CreateMap<ResumedReadAssistanceTypeOutput, ResumedReadAssistanceTypeResponse>().ReverseMap();
            CreateMap<DetailedReadAssistanceTypeOutput, DetailedReadAssistanceTypeResponse>().ReverseMap();
        }
    }
}