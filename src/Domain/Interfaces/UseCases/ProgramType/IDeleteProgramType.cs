using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases.ProgramType
{
    public interface IDeleteProgramType
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id);
    }
}