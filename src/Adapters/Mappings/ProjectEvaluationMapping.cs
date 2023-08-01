using Adapters.Gateways.ProjectEvaluation;
using AutoMapper;
using Domain.UseCases.Ports.ProjectEvaluation;

namespace Adapters.Mappings
{
    public class ProjectEvaluationMapping : Profile
    {
        public ProjectEvaluationMapping()
        {
            _ = CreateMap<EvaluateAppealProjectInput, EvaluateAppealProjectRequest>()
                .ReverseMap();
            _ = CreateMap<EvaluateSubmissionProjectInput, EvaluateSubmissionProjectRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<DetailedReadProjectEvaluationResponse, DetailedReadProjectEvaluationOutput>()
                .ReverseMap();
        }
    }
}