using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases
{
    public interface IGetProgramTypeById
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id);
    }
}