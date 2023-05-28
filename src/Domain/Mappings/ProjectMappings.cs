using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities;

namespace Domain.Mappings
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            // Projects
            CreateMap<Project, OpenProjectInput>().ReverseMap();
            CreateMap<Project, UpdateProjectInput>().ReverseMap();
            CreateMap<Project, ResumedReadProjectOutput>().ReverseMap();
            CreateMap<Project, DetailedReadProjectOutput>().ReverseMap();

            // ProjectEvaluations
            CreateMap<ProjectEvaluation, EvaluateSubmissionProjectInput>().ReverseMap();
            CreateMap<ProjectEvaluation, EvaluateAppealProjectInput>().ReverseMap();
        }
    }
}