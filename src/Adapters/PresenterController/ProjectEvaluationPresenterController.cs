using Adapters.Gateways.Project;
using Adapters.Gateways.ProjectEvaluation;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.ProjectEvaluation;
using Domain.UseCases.Ports.ProjectEvaluation;

namespace Adapters.PresenterController
{
    public class ProjectEvaluationPresenterController : IProjectEvaluationPresenterController
    {
        #region Global Scope
        private readonly IEvaluateAppealProject _evaluateAppealProject;
        private readonly IEvaluateSubmissionProject _evaluateSubmissionProject;
        private readonly IGetEvaluationByProjectId _getEvaluationByProjectId;
        private readonly IMapper _mapper;

        public ProjectEvaluationPresenterController(
            IEvaluateAppealProject evaluateAppealProject,
            IEvaluateSubmissionProject evaluateSubmissionProject,
            IGetEvaluationByProjectId getEvaluationByProjectId,
            IMapper mapper)
        {
            _evaluateAppealProject = evaluateAppealProject;
            _evaluateSubmissionProject = evaluateSubmissionProject;
            _getEvaluationByProjectId = getEvaluationByProjectId;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectResponse> EvaluateAppealProject(EvaluateAppealProjectRequest request)
        {
            EvaluateAppealProjectInput input = _mapper.Map<EvaluateAppealProjectInput>(request);
            Domain.UseCases.Ports.Project.DetailedReadProjectOutput output = await _evaluateAppealProject.ExecuteAsync(input);
            return _mapper.Map<DetailedReadProjectResponse>(output);
        }

        public async Task<DetailedReadProjectResponse> EvaluateSubmissionProject(EvaluateSubmissionProjectRequest request)
        {
            EvaluateSubmissionProjectInput input = _mapper.Map<EvaluateSubmissionProjectInput>(request);
            Domain.UseCases.Ports.Project.DetailedReadProjectOutput output = await _evaluateSubmissionProject.ExecuteAsync(input);
            return _mapper.Map<DetailedReadProjectResponse>(output);
        }

        public async Task<DetailedReadProjectEvaluationResponse> GetEvaluationByProjectId(Guid? projectId)
        {
            DetailedReadProjectEvaluationOutput output = await _getEvaluationByProjectId.ExecuteAsync(projectId);
            return _mapper.Map<DetailedReadProjectEvaluationResponse>(output);
        }
    }
}