using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface IUpdateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> Execute(Guid? id, UpdateStudentDocumentsInput model);
    }
}