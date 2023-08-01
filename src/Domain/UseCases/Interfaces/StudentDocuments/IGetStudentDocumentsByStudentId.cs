using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface IGetStudentDocumentsByStudentId
    {
        Task<ResumedReadStudentDocumentsOutput> Execute(Guid? studentId);
    }
}