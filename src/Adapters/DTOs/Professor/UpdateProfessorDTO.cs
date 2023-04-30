using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Professor
{
    public class UpdateProfessorDTO : RequestDTO
    {
        public Guid? Id { get; set; }

        #region Required Properties
        [Required]
        public string? SIAPEEnrollment { get; set; }
        [Required]
        public long IdentifyLattes { get; set; }
        #endregion
    }
}