using Application.Ports.ProgramType;

namespace Application.Interfaces.UseCases.ProgramType
{
    public interface IDeleteProgramType
    {
        Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id);
    }
}