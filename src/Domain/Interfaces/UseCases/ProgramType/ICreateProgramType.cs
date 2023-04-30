using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateProgramType
    {
        Task<DetailedReadProgramTypeOutput> Execute(CreateProgramTypeInput model);
    }
}