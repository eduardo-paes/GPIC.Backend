using Domain.Entities.Enums;

namespace Application.Tests.Mocks
{
    public class ProjectPartialReportMock
    {
        public static Domain.Entities.ProjectPartialReport MockValidProjectPartialReport()
        {
            return new Domain.Entities.ProjectPartialReport(
                projectId: Guid.NewGuid(),
                currentDevelopmentStage: 1,
                scholarPerformance: EScholarPerformance.Good,
                additionalInfo: "Additional Information",
                userId: Guid.NewGuid());
        }
    }
}