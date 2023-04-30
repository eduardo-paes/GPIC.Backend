using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteProgramType
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id);
    }
}