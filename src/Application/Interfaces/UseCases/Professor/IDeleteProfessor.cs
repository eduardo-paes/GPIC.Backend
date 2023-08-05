using Application.Ports.Professor;

namespace Application.Interfaces.UseCases.Professor
{
    public interface IDeleteProfessor
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id);
    }
}