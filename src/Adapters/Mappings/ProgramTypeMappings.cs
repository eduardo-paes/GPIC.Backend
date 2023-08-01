using Adapters.Gateways.ProgramType;
using AutoMapper;
using Domain.UseCases.Ports.ProgramType;

namespace Adapters.Mappings
{
    public class ProgramTypeMappings : Profile
    {
        public ProgramTypeMappings()
        {
            _ = CreateMap<CreateProgramTypeInput, CreateProgramTypeRequest>().ReverseMap();
            _ = CreateMap<UpdateProgramTypeInput, UpdateProgramTypeRequest>().ReverseMap();
            _ = CreateMap<ResumedReadProgramTypeOutput, ResumedReadProgramTypeResponse>().ReverseMap();
            _ = CreateMap<DetailedReadProgramTypeOutput, DetailedReadProgramTypeResponse>().ReverseMap();
        }
    }
}