using Application.Ports.StudentDocuments;

namespace Application.Interfaces.UseCases.StudentDocuments
{
    public interface IGetStudentDocumentsByStudentId
    {
        Task<ResumedReadStudentDocumentsOutput> ExecuteAsync(Guid? studentId);
    }
}