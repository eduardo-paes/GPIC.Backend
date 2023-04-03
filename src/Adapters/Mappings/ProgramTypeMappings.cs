using AutoMapper;
using Adapters.DTOs.ProgramType;
using Domain.Contracts.ProgramType;

namespace Adapters.Mappings
{
    public class ProgramTypeMappings : Profile
    {
        public ProgramTypeMappings()
        {
            CreateMap<CreateProgramTypeInput, CreateProgramTypeDTO>().ReverseMap();
            CreateMap<UpdateProgramTypeInput, UpdateProgramTypeDTO>().ReverseMap();
            CreateMap<ResumedReadProgramTypeOutput, ResumedReadProgramTypeDTO>().ReverseMap();
            CreateMap<DetailedReadProgramTypeOutput, DetailedReadProgramTypeDTO>().ReverseMap();
        }
    }
}