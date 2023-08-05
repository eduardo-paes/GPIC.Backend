using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface IGetOpenProjects
    {
        Task<IList<ResumedReadProjectOutput>> ExecuteAsync(int skip, int take, bool onlyMyProjects = true);
    }
}