namespace Domain.UseCases.Ports.StudentDocuments
{
    public class DetailedReadStudentDocumentsOutput : BaseStudentDocumentsOutput
    {
        public DateTime? DeletedAt { get; set; }
    }
}