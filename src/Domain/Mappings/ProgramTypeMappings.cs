using AutoMapper;
using Domain.Contracts.ProgramType;
using Domain.Entities;

namespace Domain.Mappings
{
    public class ProgramTypeMappings : Profile
    {
        public ProgramTypeMappings()
        {
            CreateMap<ProgramType, CreateProgramTypeInput>().ReverseMap();
            CreateMap<ProgramType, UpdateProgramTypeInput>().ReverseMap();
            CreateMap<ProgramType, ResumedReadProgramTypeOutput>().ReverseMap();
            CreateMap<ProgramType, DetailedReadProgramTypeOutput>().ReverseMap();
        }
    }
}