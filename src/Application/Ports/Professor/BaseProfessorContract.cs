using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Professor
{
    public abstract class BaseProfessorContract
    {
        [Required]
        public string? SIAPEEnrollment { get; set; }
        [Required]
        public long IdentifyLattes { get; set; }
    }
}