using Adapters.Gateways.ProjectEvaluation;
using AutoMapper;
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