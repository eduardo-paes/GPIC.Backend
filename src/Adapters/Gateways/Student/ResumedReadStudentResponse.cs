using Adapters.Gateways.Base;

namespace Adapters.Gateways.Student
{
    public class ResumedReadStudentResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        #region Required Properties
        public DateTime BirthDate { get; set; }
        public long RG { get; set; }
        public string? IssuingAgency { get; set; }
        public DateTime DispatchDate { get; set; }
        public int Gender { get; set; }
        public int Race { get; set; }
        public string? HomeAddress { get; set; }
        public string? City { get; set; }
        public string? UF { get; set; }
        public long CEP { get; set; }
        public Guid? CampusId { get; set; }
        public Guid? CourseId { get; set; }
        public string? StartYear { get; set; }
        public Guid? StudentAssistanceScholarshipId { get; set; }
        #endregion

        #region Optional Properties
        public int? PhoneDDD { get; set; }
        public long? Phone { get; set; }
        public int? CellPhoneDDD { get; set; }
        public long? CellPhone { get; set; }
        #endregion
    }
}