using AutoMapper;
using Domain.Entities;
using Application.Ports.ProgramType;

namespace Domain.Mappings
{
    public class ProgramTypeMappings : Profile
    {
        public ProgramTypeMappings()
        {
            _ = CreateMap<ProgramType, CreateProgramTypeInput>().ReverseMap();
            _ = CreateMap<ProgramType, UpdateProgramTypeInput>().ReverseMap();
            _ = CreateMap<ProgramType, ResumedReadProgramTypeOutput>().ReverseMap();
            _ = CreateMap<ProgramType, DetailedReadProgramTypeOutput>().ReverseMap();
        }
    }
}