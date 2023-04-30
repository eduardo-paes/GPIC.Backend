using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Professor
{
    public class CreateProfessorInput : BaseProfessorContract
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
        #endregion
    }
}