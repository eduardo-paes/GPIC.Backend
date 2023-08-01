using Domain.UseCases.Ports.ProgramType;

namespace Domain.UseCases.Interfaces.ProgramType
{
    public interface IGetProgramTypeById
    {
        Task<DetailedReadProgramTypeOutput> Execute(Guid? id);
    }
}