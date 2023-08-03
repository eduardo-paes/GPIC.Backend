using Domain.UseCases.Ports.ProgramType;

namespace Domain.UseCases.Interfaces.ProgramType
{
    public interface IUpdateProgramType
    {
        Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id, UpdateProgramTypeInput input);
    }
}