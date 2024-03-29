using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Student
{
    public abstract class BaseStudentContract
    {
        #region Required Properties
        [Required]
        public string? RegistrationCode { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public long RG { get; set; }
        [Required]
        public string? IssuingAgency { get; set; }
        [Required]
        public DateTime DispatchDate { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public int Race { get; set; }
        [Required]
        public string? HomeAddress { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? UF { get; set; }
        [Required]
        public long CEP { get; set; }
        [Required]
        public Guid? CampusId { get; set; }
        [Required]
        public Guid? CourseId { get; set; }
        [Required]
        public string? StartYear { get; set; }
        [Required]
        public Guid? AssistanceTypeId { get; set; }
        #endregion Required Properties

        #region Optional Properties
        public int? PhoneDDD { get; set; }
        public long? Phone { get; set; }
        public int? CellPhoneDDD { get; set; }
        public long? CellPhone { get; set; }
        #endregion Optional Properties
    }
}