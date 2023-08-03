using Adapters.Gateways.Professor;
using AutoMapper;
using Domain.UseCases.Ports.Professor;

namespace Adapters.Mappings
{
    public class ProfessorMappings : Profile
    {
        public ProfessorMappings()
        {
            _ = CreateMap<CreateProfessorInput, CreateProfessorRequest>().ReverseMap();
            _ = CreateMap<UpdateProfessorInput, UpdateProfessorRequest>().ReverseMap();
            _ = CreateMap<ResumedReadProfessorOutput, ResumedReadProfessorResponse>().ReverseMap();
            _ = CreateMap<DetailedReadProfessorOutput, DetailedReadProfessorResponse>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}