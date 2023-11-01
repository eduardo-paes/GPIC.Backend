using Domain.Entities;
using Domain.Entities.Enums;

namespace Application.Tests.Mocks
{
    public static class ProjectEvaluationMock
    {
        public static ProjectEvaluation MockValidProjectEvaluation() =>
            new(
                Guid.NewGuid(),
                true,
                Guid.NewGuid(),
                EProjectStatus.Accepted,
                DateTime.UtcNow,
                "Valid Submission Evaluation Description",
                EQualification.Doctor,
                EScore.Excellent,
                EScore.Excellent,
                EScore.Excellent,
                EScore.Excellent,
                10.0);
    }
}