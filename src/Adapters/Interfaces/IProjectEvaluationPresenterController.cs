using Adapters.Gateways.Project;
using Adapters.Gateways.ProjectEvaluation;

namespace Adapters.Interfaces
{
    public interface IProjectEvaluationPresenterController
    {
        Task<DetailedReadProjectResponse> EvaluateAppealProject(EvaluateAppealProjectRequest request);
        Task<DetailedReadProjectResponse> EvaluateSubmissionProject(EvaluateSubmissionProjectRequest request);
        Task<DetailedReadProjectEvaluationResponse> GetEvaluationByProjectId(Guid? projectId);
    }
}