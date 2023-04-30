using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases.Professor;
public interface IUpdateProfessor
{
    Task<DetailedReadProfessorOutput> Execute(Guid? id, UpdateProfessorInput model);
}