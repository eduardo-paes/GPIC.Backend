using Adapters.Gateways.Project;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Project;
using Domain.Interfaces.UseCases.Project;

namespace Adapters.PresenterController
{
    public class ProjectPresenterController : IProjectPresenterController
    {
        #region Global Scope
        private readonly ICancelProject _cancelProject;
        private readonly IGetClosedProjects _getClosedProjects;
        private readonly IGetOpenProjects _getOpenProjects;
        private readonly IGetProjectById _getProjectById;
        private readonly IOpenProject _openProject;
        private readonly IUpdateProject _updateProject;
        private readonly IAppealProject _appealProject;
        private readonly ISubmitProject _submitProject;
        private readonly IMapper _mapper;

        public ProjectPresenterController(
            ICancelProject cancelProject,
            IGetClosedProjects getClosedProjects,
            IGetOpenProjects getOpenProjects,
            IGetProjectById getProjectById,
            IOpenProject openProject,
            IUpdateProject updateProject,
            IAppealProject appealProject,
            ISubmitProject submitProject,
            IMapper mapper)
        {
            _cancelProject = cancelProject;
            _getClosedProjects = getClosedProjects;
            _getOpenProjects = getOpenProjects;
            _getProjectById = getProjectById;
            _openProject = openProject;
            _updateProject = updateProject;
            _appealProject = appealProject;
            _submitProject = submitProject;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResumedReadProjectResponse> AppealProject(Guid? projectId, string? appealDescription)
        {
            var output = await _appealProject.Execute(projectId, appealDescription);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> CancelProject(Guid? id, string? observation)
        {
            var output = await _cancelProject.Execute(id, observation);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<IList<ResumedReadProjectResponse>> GetClosedProjects(int skip, int take, bool onlyMyProjects = true)
        {
            var output = await _getClosedProjects.Execute(skip, take, onlyMyProjects);
            return _mapper.Map<IList<ResumedReadProjectResponse>>(output);
        }

        public async Task<IList<ResumedReadProjectResponse>> GetOpenProjects(int skip, int take, bool onlyMyProjects = true)
        {
            var output = await _getOpenProjects.Execute(skip, take, onlyMyProjects);
            return _mapper.Map<IList<ResumedReadProjectResponse>>(output);
        }

        public async Task<DetailedReadProjectResponse> GetProjectById(Guid? id)
        {
            var output = await _getProjectById.Execute(id);
            return _mapper.Map<DetailedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> OpenProject(OpenProjectRequest request)
        {
            var input = _mapper.Map<OpenProjectInput>(request);
            var output = await _openProject.Execute(input);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> SubmitProject(Guid? projectId)
        {
            var output = await _submitProject.Execute(projectId);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> UpdateProject(Guid? id, UpdateProjectRequest request)
        {
            var input = _mapper.Map<UpdateProjectInput>(request);
            var output = await _updateProject.Execute(id, input);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }
    }
}