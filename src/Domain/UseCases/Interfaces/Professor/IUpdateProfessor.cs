using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface IUpdateProfessor
    {
        Task<DetailedReadProfessorOutput> Execute(Guid? id, UpdateProfessorInput model);
    }
}