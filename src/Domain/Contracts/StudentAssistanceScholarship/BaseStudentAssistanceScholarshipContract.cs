using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.StudentAssistanceScholarship
{
    public abstract class BaseStudentAssistanceScholarshipContract
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}