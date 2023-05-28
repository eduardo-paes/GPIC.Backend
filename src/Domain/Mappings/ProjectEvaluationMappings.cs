using AutoMapper;
using Domain.Contracts.ProjectEvaluation;
using Domain.Entities;

namespace Domain.Mappings
{
    public class ProjectEvaluationMappings : Profile
    {
        public ProjectEvaluationMappings()
        {
            CreateMap<ProjectEvaluation, EvaluateSubmissionProjectInput>().ReverseMap();
            CreateMap<ProjectEvaluation, EvaluateAppealProjectInput>().ReverseMap();
        }
    }
}