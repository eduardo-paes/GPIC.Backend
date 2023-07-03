using Adapters.Gateways.Project;
using Adapters.Gateways.ProjectEvaluation;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.ProjectEvaluation;
using Domain.Interfaces.UseCases.ProjectEvaluation;

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
        #endregion

        public async Task<DetailedReadProjectResponse> EvaluateAppealProject(EvaluateAppealProjectRequest request)
        {
            var input = _mapper.Map<EvaluateAppealProjectInput>(request);
            var output = await _evaluateAppealProject.Execute(input);
            return _mapper.Map<DetailedReadProjectResponse>(output);
        }

        public async Task<DetailedReadProjectResponse> EvaluateSubmissionProject(EvaluateSubmissionProjectRequest request)
        {
            var input = _mapper.Map<EvaluateSubmissionProjectInput>(request);
            var output = await _evaluateSubmissionProject.Execute(input);
            return _mapper.Map<DetailedReadProjectResponse>(output);
        }

        public async Task<DetailedReadProjectEvaluationResponse> GetEvaluationByProjectId(Guid? projectId)
        {
            var output = await _getEvaluationByProjectId.Execute(projectId);
            return _mapper.Map<DetailedReadProjectEvaluationResponse>(output);
        }
    }
}