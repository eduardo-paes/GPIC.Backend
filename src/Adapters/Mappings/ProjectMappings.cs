using AutoMapper;
using Adapters.Gateways.Project;
using Domain.Contracts.Project;

namespace Adapters.Mappings
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            CreateMap<OpenProjectInput, OpenProjectRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            CreateMap<UpdateProjectInput, UpdateProjectRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            CreateMap<ResumedReadProjectOutput, ResumedReadProjectResponse>().ReverseMap();
            CreateMap<DetailedReadProjectOutput, DetailedReadProjectResponse>().ReverseMap();
        }
    }
}