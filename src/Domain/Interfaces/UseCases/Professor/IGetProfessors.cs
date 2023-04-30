using Domain.Contracts.Professor;

namespace Domain.Interfaces.UseCases.Professor;
public interface IGetProfessors
{
    Task<IQueryable<ResumedReadProfessorOutput>> Execute(int skip, int take);
}