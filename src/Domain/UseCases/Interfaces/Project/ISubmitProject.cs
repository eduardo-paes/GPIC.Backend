using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface ISubmitProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? projectId);
    }
}