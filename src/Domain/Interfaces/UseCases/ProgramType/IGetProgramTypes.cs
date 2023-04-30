using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases
{
    public interface IGetProgramTypes
    {
        Task<IQueryable<ResumedReadProgramTypeOutput>> Execute(int skip, int take);
    }
}