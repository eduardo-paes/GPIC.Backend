using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.ProjectEvaluation;

namespace Domain.Mappings
{
    public class ProjectEvaluationMappings : Profile
    {
        public ProjectEvaluationMappings()
        {
            _ = CreateMap<ProjectEvaluation, EvaluateSubmissionProjectInput>().ReverseMap();
            _ = CreateMap<ProjectEvaluation, EvaluateAppealProjectInput>().ReverseMap();
        }
    }
}