using AutoMapper;
using Adapters.Gateways.ProjectActivity;
using Domain.Contracts.ProjectActivity;

namespace Adapters.Mappings
{
    public class ProjectActivityMappings : Profile
    {
        public ProjectActivityMappings()
        {
            CreateMap<CreateProjectActivityInput, CreateProjectActivityRequest>().ReverseMap();
            CreateMap<UpdateProjectActivityInput, UpdateProjectActivityRequest>().ReverseMap();
            CreateMap<ResumedReadProjectActivityOutput, ResumedReadProjectActivityResponse>().ReverseMap();
            CreateMap<DetailedReadProjectActivityOutput, DetailedReadProjectActivityResponse>().ReverseMap();
        }
    }
}