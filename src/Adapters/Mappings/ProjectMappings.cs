using Adapters.Gateways.Project;
using AutoMapper;
using Domain.UseCases.Ports.Project;

namespace Adapters.Mappings
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            _ = CreateMap<OpenProjectInput, OpenProjectRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<UpdateProjectInput, UpdateProjectRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<ResumedReadProjectOutput, ResumedReadProjectResponse>().ReverseMap();
            _ = CreateMap<DetailedReadProjectOutput, DetailedReadProjectResponse>().ReverseMap();
        }
    }
}