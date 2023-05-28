using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities;

namespace Domain.Mappings
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            CreateMap<Project, OpenProjectInput>().ReverseMap();
            CreateMap<Project, UpdateProjectInput>().ReverseMap();
            CreateMap<Project, ResumedReadProjectOutput>().ReverseMap();
            CreateMap<Project, DetailedReadProjectOutput>().ReverseMap();
        }
    }
}