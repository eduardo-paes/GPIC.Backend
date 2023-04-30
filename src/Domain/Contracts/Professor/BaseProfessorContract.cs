using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Professor
{
    public class BaseProfessorContract
    {
        [Required]
        public string? SIAPEEnrollment { get; set; }
        [Required]
        public long IdentifyLattes { get; set; }
    }
}