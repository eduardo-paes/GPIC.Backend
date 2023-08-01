using Domain.UseCases.Ports.ProgramType;

namespace Domain.UseCases.Interfaces.ProgramType
{
    public interface IGetProgramTypes
    {
        Task<IQueryable<ResumedReadProgramTypeOutput>> Execute(int skip, int take);
    }
}