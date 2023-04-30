using Adapters.DTOs.Base;

namespace Adapters.DTOs.Professor
{
    public class ResumedReadProfessorDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        #region Required Properties
        public string? SIAPEEnrollment { get; set; }
        public long IdentifyLattes { get; set; }
        public int ProfessorAssistanceProgram { get; set; }
        #endregion
    }
}