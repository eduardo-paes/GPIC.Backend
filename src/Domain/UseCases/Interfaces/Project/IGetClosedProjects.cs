using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IGetClosedProjects
    {
        Task<IList<ResumedReadProjectOutput>> Execute(int skip, int take, bool onlyMyProjects = true);
    }
}