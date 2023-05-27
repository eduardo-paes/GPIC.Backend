using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Professor
{
    public class UpdateProfessorRequest : Request
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