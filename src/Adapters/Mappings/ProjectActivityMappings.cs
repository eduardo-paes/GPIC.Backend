using Adapters.Gateways.ProjectActivity;
using AutoMapper;
using Domain.UseCases.Ports.ProjectActivity;

namespace Adapters.Mappings
{
    public class ProjectActivityMappings : Profile
    {
        public ProjectActivityMappings()
        {
            _ = CreateMap<CreateProjectActivityInput, CreateProjectActivityRequest>().ReverseMap();
            _ = CreateMap<UpdateProjectActivityInput, UpdateProjectActivityRequest>().ReverseMap();
            _ = CreateMap<EvaluateProjectActivityInput, EvaluateProjectActivityRequest>().ReverseMap();
            _ = CreateMap<ResumedReadProjectActivityOutput, ResumedReadProjectActivityResponse>().ReverseMap();
            _ = CreateMap<DetailedReadProjectActivityOutput, DetailedReadProjectActivityResponse>().ReverseMap();
        }
    }
}