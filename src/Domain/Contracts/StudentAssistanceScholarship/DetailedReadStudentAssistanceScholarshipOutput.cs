namespace Domain.Contracts.StudentAssistanceScholarship
{
    public class DetailedReadStudentAssistanceScholarshipOutput : BaseStudentAssistanceScholarshipContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}