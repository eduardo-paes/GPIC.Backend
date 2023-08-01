using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.Professor;

namespace Domain.Mappings
{
    public class ProfessorMappings : Profile
    {
        public ProfessorMappings()
        {
            _ = CreateMap<Professor, CreateProfessorInput>().ReverseMap();
            _ = CreateMap<Professor, UpdateProfessorInput>().ReverseMap();

            _ = CreateMap<Professor, ResumedReadProfessorOutput>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ReverseMap();

            _ = CreateMap<Professor, DetailedReadProfessorOutput>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}