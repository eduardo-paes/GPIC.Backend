using AutoMapper;
using Adapters.Gateways.ProgramType;
using Domain.Contracts.ProgramType;

namespace Adapters.Mappings
{
    public class ProgramTypeMappings : Profile
    {
        public ProgramTypeMappings()
        {
            CreateMap<CreateProgramTypeInput, CreateProgramTypeRequest>().ReverseMap();
            CreateMap<UpdateProgramTypeInput, UpdateProgramTypeRequest>().ReverseMap();
            CreateMap<ResumedReadProgramTypeOutput, ResumedReadProgramTypeResponse>().ReverseMap();
            CreateMap<DetailedReadProgramTypeOutput, DetailedReadProgramTypeResponse>().ReverseMap();
        }
    }
}