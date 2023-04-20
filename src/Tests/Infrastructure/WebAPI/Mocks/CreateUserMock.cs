using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Tests.Infrastructure.WebAPI.Mocks
{
    public class CreateUserMock : RequestDTO
    {
        public string? Name { get; set; } = "John Doe";
        public string? CPF { get; set; } = "12345678910";
        public string? Email { get; set; } = "johndoe@example.com";
        public string? Password { get; set; } = "password";

        public DateTime BirthDate { get; set; } = new DateTime(2000, 1, 1);
        public long RG { get; set; } = 123456789;
        public string? IssuingAgency { get; set; } = "SSP";
        public DateTime DispatchDate { get; set; } = new DateTime(2020, 1, 1);
        public int Gender { get; set; } = 1;
        public int Race { get; set; } = 1;
        public string? HomeAddress { get; set; } = "Street 123";
        public string? City { get; set; } = "City";
        public string? UF { get; set; } = "UF";
        public long CEP { get; set; } = 12345678;
        public Guid? CampusId { get; set; } = Guid.NewGuid();
        public Guid? CourseId { get; set; } = Guid.NewGuid();
        public string? StartYear { get; set; } = "2022";
        public int StudentAssistanceProgram { get; set; } = 1;

        public int? PhoneDDD { get; set; } = 11;
        public long? Phone { get; set; } = 123456789;
        public int? CellPhoneDDD { get; set; } = 11;
        public long? CellPhone { get; set; } = 987654321;
    }
}