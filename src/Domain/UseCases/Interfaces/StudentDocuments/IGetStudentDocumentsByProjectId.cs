using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface IGetStudentDocumentsByProjectId
    {
        Task<ResumedReadStudentDocumentsOutput> Execute(Guid? projectId);
    }
}