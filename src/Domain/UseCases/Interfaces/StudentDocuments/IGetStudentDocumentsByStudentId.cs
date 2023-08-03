using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface IGetStudentDocumentsByStudentId
    {
        Task<ResumedReadStudentDocumentsOutput> ExecuteAsync(Guid? studentId);
    }
}