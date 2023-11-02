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
            _ = CreateMap<Project, DetailedReadProjectOutput>().ReverseMap();
            _ = CreateMap<Project, ResumedReadProjectOutput>()
                .ForMember(dest => dest.ProfessorName, opt => opt.MapFrom(src => src.Professor.User.Name))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.User.Name))
                    .ReverseMap();

        }
    }
}