using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases.ProgramType
{
    public interface ICreateProgramType
    {
        Task<DetailedReadProgramTypeOutput> Execute(CreateProgramTypeInput model);
    }
}