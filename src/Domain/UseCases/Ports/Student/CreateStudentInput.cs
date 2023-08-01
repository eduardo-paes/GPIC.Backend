using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.Student
{
    public class CreateStudentInput : BaseStudentContract
    {
        #region User Properties
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? CPF { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        #endregion User Properties
    }
}