using Application.Ports.ProgramType;

namespace Application.Interfaces.UseCases.ProgramType
{
    public interface IUpdateProgramType
    {
        Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id, UpdateProgramTypeInput input);
    }
}