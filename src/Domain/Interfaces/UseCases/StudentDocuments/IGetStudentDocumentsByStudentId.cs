using Domain.Contracts.StudentDocuments;

namespace Domain.Interfaces.UseCases
{
    public interface IGetStudentDocumentsByStudentId
    {
        Task<ResumedReadStudentDocumentsOutput> Execute(Guid? studentId);
    }
}