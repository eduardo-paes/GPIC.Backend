using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IGetOpenProjects
    {
        Task<IList<ResumedReadProjectOutput>> Execute(int skip, int take, bool onlyMyProjects = true);
    }
}