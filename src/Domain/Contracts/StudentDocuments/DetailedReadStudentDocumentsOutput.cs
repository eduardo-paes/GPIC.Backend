namespace Domain.Contracts.StudentDocuments;
public class DetailedReadStudentDocumentsOutput : BaseStudentDocumentsOutput
{
    public DateTime? DeletedAt { get; set; }
}