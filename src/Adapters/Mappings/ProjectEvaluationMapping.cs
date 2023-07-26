using AutoMapper;
using Adapters.Gateways.ProjectEvaluation;
using Domain.Contracts.ProjectEvaluation;

namespace Adapters.Mappings
{
    public class ProjectEvaluationMapping : Profile
    {
        public ProjectEvaluationMapping()
        {
            CreateMap<EvaluateAppealProjectInput, EvaluateAppealProjectRequest>()
                .ReverseMap();
            CreateMap<EvaluateSubmissionProjectInput, EvaluateSubmissionProjectRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            CreateMap<DetailedReadProjectEvaluationResponse, DetailedReadProjectEvaluationOutput>()
                .ReverseMap();
        }
    }
}