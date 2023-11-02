using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProjectEvaluation
{
    public class EvaluateStudentDocumentsInput
    {
        [Required]
        public Guid? ProjectId { get; set; }
        [Required]
        public bool? IsDocumentsApproved { get; set; }
        [Required]
        public string? DocumentsEvaluationDescription { get; set; }
    }
}