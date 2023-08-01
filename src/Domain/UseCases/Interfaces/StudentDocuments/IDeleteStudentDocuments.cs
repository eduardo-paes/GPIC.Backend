using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface IDeleteStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> Execute(Guid? id);
    }
}