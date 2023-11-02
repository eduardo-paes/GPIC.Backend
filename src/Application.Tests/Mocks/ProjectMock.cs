using Domain.Entities;
using Domain.Entities.Enums;

namespace Application.Tests.Mocks
{
    public static class ProjectMock
    {
        public static Project MockValidProject() => new(
            "Project Title",
            "Keyword 1",
            "Keyword 2",
            "Keyword 3",
            true,
            "Objective",
            "Methodology",
            "Expected Results",
            "Schedule",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            EProjectStatus.Opened,
            "Status Description",
            "Appeal Observation",
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Cancellation Reason");

        public static Project MockValidProjectWithId() => new(
            Guid.NewGuid(),
            "Project Title",
            "Keyword 1",
            "Keyword 2",
            "Keyword 3",
            true,
            "Objective",
            "Methodology",
            "Expected Results",
            "Schedule",
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            EProjectStatus.Opened,
            "Status Description",
            "Appeal Observation",
            DateTime.UtcNow,
            DateTime.UtcNow,
            DateTime.UtcNow,
            "Cancellation Reason");

        public static Project MockValidProjectProfessorAndNotice()
        {
            return new(
                Guid.NewGuid(),
                "Project Title",
                "Keyword 1",
                "Keyword 2",
                "Keyword 3",
                true,
                "Objective",
                "Methodology",
                "Expected Results",
                "Schedule",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                EProjectStatus.Opened,
                "Status Description",
                "Appeal Observation",
                DateTime.UtcNow,
                DateTime.UtcNow,
                DateTime.UtcNow,
                "Cancellation Reason")
            {
                SubArea = new SubArea(Guid.NewGuid(), "SubArea Name", "SubArea Code"),
                ProgramType = new ProgramType("Program Type Name", "Program Type Description"),
                Professor = new Professor("1234567", 1234567)
                {
                    User = new User("Name", "professor@email.com", "Password", "58411338029", ERole.ADMIN)
                },
                Notice = new(
                    id: Guid.NewGuid(),
                    registrationStartDate: DateTime.UtcNow,
                    registrationEndDate: DateTime.UtcNow.AddDays(7),
                    evaluationStartDate: DateTime.UtcNow.AddDays(8),
                    evaluationEndDate: DateTime.UtcNow.AddDays(14),
                    appealStartDate: DateTime.UtcNow.AddDays(15),
                    appealFinalDate: DateTime.UtcNow.AddDays(21),
                    sendingDocsStartDate: DateTime.UtcNow.AddDays(22),
                    sendingDocsEndDate: DateTime.UtcNow.AddDays(28),
                    partialReportDeadline: DateTime.UtcNow.AddDays(29),
                    finalReportDeadline: DateTime.UtcNow.AddDays(35),
                    description: "Edital de teste",
                    suspensionYears: 1
                )
            };
        }
    }
}