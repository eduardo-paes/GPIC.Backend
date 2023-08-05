using Application.Ports.Professor;

namespace Application.Interfaces.UseCases.Professor
{
    public interface IGetProfessors
    {
        Task<IQueryable<ResumedReadProfessorOutput>> ExecuteAsync(int skip, int take);
    }
}