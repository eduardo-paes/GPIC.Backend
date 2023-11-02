using Application.Ports.Professor;

namespace Application.Interfaces.UseCases.Professor
{
    public interface IUpdateProfessor
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id, UpdateProfessorInput model);
    }
}