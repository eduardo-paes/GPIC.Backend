using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface IGetProfessorById
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id);
    }
}