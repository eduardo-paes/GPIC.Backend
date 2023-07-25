using AutoMapper;
using Adapters.Gateways.ProjectEvaluation;
using Domain.Contracts.ProjectEvaluation;

namespace Adapters.Mappings
{
    public class ProjectEvaluationMapping : Profile
    {
        public ProjectEvaluationMapping()
        {
            CreateMap<EvaluateAppealProjectInput, EvaluateAppealProjectRequest>();
            CreateMap<EvaluateSubmissionProjectInput, EvaluateSubmissionProjectRequest>();
            CreateMap<DetailedReadProjectEvaluationResponse, DetailedReadProjectEvaluationOutput>();
        }
    }
}