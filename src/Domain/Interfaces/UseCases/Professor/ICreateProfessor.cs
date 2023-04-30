using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases.Professor;
public interface ICreateProfessor
{
    Task<DetailedReadProfessorOutput> Execute(CreateProfessorInput model);
}