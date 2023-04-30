using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateProgramType
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id, UpdateProgramTypeInput model);
    }
}