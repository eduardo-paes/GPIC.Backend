using AutoMapper;
using Domain.Contracts.Professor;
using Domain.Entities;

namespace Domain.Mappings
{
    public class ProfessorMappings : Profile
    {
        public ProfessorMappings()
        {
            CreateMap<Professor, CreateProfessorInput>().ReverseMap();
            CreateMap<Professor, UpdateProfessorInput>().ReverseMap();

            CreateMap<Professor, ResumedReadProfessorOutput>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ReverseMap();

            CreateMap<Professor, DetailedReadProfessorOutput>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}