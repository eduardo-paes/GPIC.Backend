using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases
{
    public interface IGetClosedProjects
    {
        Task<IList<ResumedReadProjectOutput>> Execute(int? skip, int? take, bool onlyMyProjects = true);
    }
}