using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface ISubmitDocuments
    {
        Task<ProjectDocumentsOutput> Execute(ProjectDocumentsInput input);
    }
}