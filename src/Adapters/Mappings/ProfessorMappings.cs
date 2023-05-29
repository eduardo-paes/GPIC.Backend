using AutoMapper;
using Adapters.Gateways.Professor;
using Domain.Contracts.Professor;

namespace Adapters.Mappings
{
    public class ProfessorMappings : Profile
    {
        public ProfessorMappings()
        {
            CreateMap<DetailedReadProfessorOutput, DetailedReadProfessorResponse>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}