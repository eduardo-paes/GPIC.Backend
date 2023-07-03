using Domain.Contracts.StudentDocuments;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> Execute(Guid? id, UpdateStudentDocumentsInput model);
    }
}