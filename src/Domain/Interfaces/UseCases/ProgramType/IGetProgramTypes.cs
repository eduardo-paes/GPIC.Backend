using Domain.Contracts.ProgramType;

namespace Domain.Interfaces.UseCases.ProgramType
{
    public interface IGetProgramTypes
    {
        Task<IQueryable<ResumedReadProgramTypeOutput>> Execute(int skip, int take);
    }
}