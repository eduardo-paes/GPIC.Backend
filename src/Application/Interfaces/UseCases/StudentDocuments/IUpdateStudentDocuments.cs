using Application.Ports.StudentDocuments;

namespace Application.Interfaces.UseCases.StudentDocuments
{
    public interface IUpdateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(Guid? id, UpdateStudentDocumentsInput model);
    }
}