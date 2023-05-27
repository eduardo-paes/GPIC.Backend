using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.Professor
{
    public class CreateProfessorRequest : Request
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

        #region Required Properties
        [Required]
        public string? SIAPEEnrollment { get; set; }
        [Required]
        public long IdentifyLattes { get; set; }
        #endregion
    }
}