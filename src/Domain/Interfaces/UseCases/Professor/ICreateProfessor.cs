using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases;
public interface ICreateProfessor
{
    Task<DetailedReadProfessorOutput> Execute(CreateProfessorInput model);
}