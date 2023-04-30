using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases.Professor;
public interface IDeleteProfessor
{
    Task<DetailedReadProfessorOutput> Execute(Guid? id);
}