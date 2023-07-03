using Domain.Contracts.StudentDocuments;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> Execute(Guid? id);
    }
}