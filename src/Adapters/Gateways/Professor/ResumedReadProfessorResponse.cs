using Adapters.Gateways.Base;

namespace Adapters.Gateways.Professor
{
    public class ResumedReadProfessorResponse : Response
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