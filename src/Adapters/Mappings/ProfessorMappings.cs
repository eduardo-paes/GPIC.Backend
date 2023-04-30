using AutoMapper;
using Adapters.DTOs.Professor;
using Domain.Contracts.Professor;

namespace Adapters.Mappings
{
    public class ProfessorMappings : Profile
    {
        public ProfessorMappings()
        {
            CreateMap<CreateProfessorInput, CreateProfessorDTO>().ReverseMap();
            CreateMap<UpdateProfessorInput, UpdateProfessorDTO>().ReverseMap();
            CreateMap<ResumedReadProfessorOutput, ResumedReadProfessorDTO>().ReverseMap();
            CreateMap<DetailedReadProfessorOutput, DetailedReadProfessorDTO>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}