using Domain.Contracts.StudentDocuments;

namespace Domain.Interfaces.UseCases
{
    public interface IGetStudentDocumentsByProjectId
    {
        Task<ResumedReadStudentDocumentsOutput> Execute(Guid? projectId);
    }
}