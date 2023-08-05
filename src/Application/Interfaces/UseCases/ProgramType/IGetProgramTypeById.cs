using Application.Ports.ProgramType;

namespace Application.Interfaces.UseCases.ProgramType
{
    public interface IGetProgramTypeById
    {
        Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id);
    }
}