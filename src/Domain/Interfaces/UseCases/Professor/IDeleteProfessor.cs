using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases;
public interface IDeleteProfessor
{
    Task<DetailedReadProfessorOutput> Execute(Guid? id);
}