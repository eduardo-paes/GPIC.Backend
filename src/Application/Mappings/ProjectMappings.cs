using AutoMapper;
using Domain.Entities;
using Application.Ports.Project;

namespace Domain.Mappings
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            _ = CreateMap<Project, OpenProjectInput>().ReverseMap();
            _ = CreateMap<Project, UpdateProjectInput>().ReverseMap();
            _ = CreateMap<Project, ResumedReadProjectOutput>().ReverseMap();
            _ = CreateMap<Project, DetailedReadProjectOutput>().ReverseMap();
        }
    }
}