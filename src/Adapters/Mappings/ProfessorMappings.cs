using AutoMapper;
using Adapters.Gateways.Professor;
using Domain.Contracts.Professor;

namespace Adapters.Mappings
{
    public class ProfessorMappings : Profile
    {
        public ProfessorMappings()
        {
            CreateMap<CreateProfessorInput, CreateProfessorRequest>().ReverseMap();
            CreateMap<UpdateProfessorInput, UpdateProfessorRequest>().ReverseMap();
            CreateMap<ResumedReadProfessorOutput, ResumedReadProfessorResponse>().ReverseMap();
            CreateMap<DetailedReadProfessorOutput, DetailedReadProfessorResponse>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}