using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases;
public interface IGetProfessorById
{
    Task<DetailedReadProfessorOutput> Execute(Guid? id);
}