using Domain.UseCases.Ports.ProgramType;

namespace Domain.UseCases.Interfaces.ProgramType
{
    public interface IGetProgramTypes
    {
        Task<IQueryable<ResumedReadProgramTypeOutput>> ExecuteAsync(int skip, int take);
    }
}