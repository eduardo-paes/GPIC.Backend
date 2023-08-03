using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface IUpdateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(Guid? id, UpdateStudentDocumentsInput model);
    }
}