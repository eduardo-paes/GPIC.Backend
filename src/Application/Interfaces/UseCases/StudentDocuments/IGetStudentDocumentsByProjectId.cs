using Application.Ports.StudentDocuments;

namespace Application.Interfaces.UseCases.StudentDocuments
{
    public interface IGetStudentDocumentsByProjectId
    {
        Task<ResumedReadStudentDocumentsOutput> ExecuteAsync(Guid? projectId);
    }
}