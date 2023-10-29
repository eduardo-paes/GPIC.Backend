using Domain.Entities.Enums;

namespace Application.Tests.Mocks
{
    public static class StudentMock
    {
        public static Domain.Entities.Student MockValidStudent() => new(
                    birthDate: new DateTime(2000, 1, 1),
                    rg: 123456789,
                    issuingAgency: "Agency",
                    dispatchDate: new DateTime(2020, 1, 1),
                    gender: EGender.M,
                    race: ERace.White,
                    homeAddress: "Street 1",
                    city: "City",
                    uf: "UF",
                    cep: 12345678,
                    phoneDDD: 11,
                    phone: 12345678,
                    cellPhoneDDD: 22,
                    cellPhone: 987654321,
                    campusId: Guid.NewGuid(),
                    courseId: Guid.NewGuid(),
                    startYear: "2022",
                    studentAssistanceProgramId: Guid.NewGuid(),
                    registrationCode: "GCOM1234567"
                );

        public static Domain.Entities.Student MockValidStudentWithId() => new(
                    id: Guid.NewGuid(),
                    birthDate: new DateTime(2000, 1, 1),
                    rg: 123456789,
                    issuingAgency: "Agency",
                    dispatchDate: new DateTime(2020, 1, 1),
                    gender: EGender.M,
                    race: ERace.White,
                    homeAddress: "Street 1",
                    city: "City",
                    uf: "UF",
                    cep: 12345678,
                    phoneDDD: 11,
                    phone: 12345678,
                    cellPhoneDDD: 22,
                    cellPhone: 987654321,
                    campusId: Guid.NewGuid(),
                    courseId: Guid.NewGuid(),
                    startYear: "2022",
                    studentAssistanceProgramId: Guid.NewGuid(),
                    registrationCode: "GCOM1234567"
                );
    }
}