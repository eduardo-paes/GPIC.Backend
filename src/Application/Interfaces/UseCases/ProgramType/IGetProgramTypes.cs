using Application.Ports.ProgramType;

namespace Application.Interfaces.UseCases.ProgramType
{
    public interface IGetProgramTypes
    {
        Task<IQueryable<ResumedReadProgramTypeOutput>> ExecuteAsync(int skip, int take);
    }
}