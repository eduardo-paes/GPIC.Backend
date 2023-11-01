using Domain.Entities;

namespace Application.Tests.Mocks
{
    public static class ProjectFinalReportMock
    {
        public static ProjectFinalReport MockValidProjectFinalReport()
        {
            return new ProjectFinalReport(Guid.NewGuid(), Guid.NewGuid())
            {
                ReportUrl = "https://example.com/report",
                SendDate = DateTime.UtcNow
            };
        }

        public static ProjectFinalReport MockValidProjectFinalReportWithId()
        {
            return new ProjectFinalReport(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
            {
                ReportUrl = "https://example.com/report",
                SendDate = DateTime.UtcNow
            };
        }
    }
}