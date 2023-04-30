using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases.Professor;
public interface IGetProfessorById
{
    Task<DetailedReadProfessorOutput> Execute(Guid? id);
}