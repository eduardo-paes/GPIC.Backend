using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases.ProgramType
{
    public interface IGetProgramTypeById
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id);
    }
}