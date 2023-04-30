using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases;
public interface IUpdateProfessor
{
    Task<DetailedReadProfessorOutput> Execute(Guid? id, UpdateProfessorInput model);
}