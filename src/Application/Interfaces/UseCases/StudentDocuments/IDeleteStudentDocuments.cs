using Application.Ports.StudentDocuments;

namespace Application.Interfaces.UseCases.StudentDocuments
{
    public interface IDeleteStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(Guid? id);
    }
}