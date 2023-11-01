using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class NoticeMock
    {
        public static Notice MockValidNotice() => new(
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
        );

        public static Notice MockValidNoticeWithId() => new(
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
        );
    }
}