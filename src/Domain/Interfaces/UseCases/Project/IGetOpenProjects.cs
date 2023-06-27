using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IGetOpenProjects
    {
        Task<IList<ResumedReadProjectOutput>> Execute(int skip, int take, bool onlyMyProjects = true);
    }
}