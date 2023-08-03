using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface IUpdateProfessor
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id, UpdateProfessorInput model);
    }
}