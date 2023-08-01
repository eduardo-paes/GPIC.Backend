using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface IGetProfessors
    {
        Task<IQueryable<ResumedReadProfessorOutput>> Execute(int skip, int take);
    }
}