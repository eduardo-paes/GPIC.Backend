using AutoMapper;
using Adapters.Gateways.Project;
using Domain.Contracts.Project;

namespace Adapters.Mappings
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            CreateMap<OpenProjectInput, OpenProjectRequest>().ReverseMap();
            CreateMap<UpdateProjectInput, UpdateProjectRequest>().ReverseMap();
            CreateMap<ResumedReadProjectOutput, ResumedReadProjectResponse>().ReverseMap();
            CreateMap<DetailedReadProjectOutput, DetailedReadProjectResponse>().ReverseMap();
        }
    }
}