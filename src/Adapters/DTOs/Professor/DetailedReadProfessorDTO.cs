using Adapters.DTOs.Base;
using Adapters.DTOs.User;

namespace Adapters.DTOs.Professor
{
    public class DetailedReadProfessorDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserReadDTO? User { get; set; }

        #region Required Properties
        public string? SIAPEEnrollment { get; set; }
        public long IdentifyLattes { get; set; }
        #endregion
    }
}