using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface IDeleteProfessor
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id);
    }
}