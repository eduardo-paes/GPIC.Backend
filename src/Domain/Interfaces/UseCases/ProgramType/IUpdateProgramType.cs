using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases.ProgramType
{
    public interface IUpdateProgramType
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id, UpdateProgramTypeInput model);
    }
}