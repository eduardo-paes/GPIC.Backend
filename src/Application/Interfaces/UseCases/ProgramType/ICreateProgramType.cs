using Application.Ports.ProgramType;

namespace Application.Interfaces.UseCases.ProgramType
{
    public interface ICreateProgramType
    {
        Task<DetailedReadProgramTypeOutput> ExecuteAsync(CreateProgramTypeInput model);
    }
}