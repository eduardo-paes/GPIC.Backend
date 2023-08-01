using Adapters.Gateways.Project;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;

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
        #endregion Global Scope

        public async Task<ResumedReadProjectResponse> AppealProject(Guid? projectId, string? appealDescription)
        {
            ResumedReadProjectOutput output = await _appealProject.Execute(projectId, appealDescription);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> CancelProject(Guid? id, string? observation)
        {
            ResumedReadProjectOutput output = await _cancelProject.Execute(id, observation);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<IList<ResumedReadProjectResponse>> GetClosedProjects(int skip, int take, bool onlyMyProjects = true)
        {
            IList<ResumedReadProjectOutput> output = await _getClosedProjects.Execute(skip, take, onlyMyProjects);
            return _mapper.Map<IList<ResumedReadProjectResponse>>(output);
        }

        public async Task<IList<ResumedReadProjectResponse>> GetOpenProjects(int skip, int take, bool onlyMyProjects = true)
        {
            IList<ResumedReadProjectOutput> output = await _getOpenProjects.Execute(skip, take, onlyMyProjects);
            return _mapper.Map<IList<ResumedReadProjectResponse>>(output);
        }

        public async Task<DetailedReadProjectResponse> GetProjectById(Guid? id)
        {
            DetailedReadProjectOutput output = await _getProjectById.Execute(id);
            return _mapper.Map<DetailedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> OpenProject(OpenProjectRequest request)
        {
            OpenProjectInput input = _mapper.Map<OpenProjectInput>(request);
            ResumedReadProjectOutput output = await _openProject.Execute(input);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> SubmitProject(Guid? projectId)
        {
            ResumedReadProjectOutput output = await _submitProject.Execute(projectId);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }

        public async Task<ResumedReadProjectResponse> UpdateProject(Guid? id, UpdateProjectRequest request)
        {
            UpdateProjectInput input = _mapper.Map<UpdateProjectInput>(request);
            ResumedReadProjectOutput output = await _updateProject.Execute(id, input);
            return _mapper.Map<ResumedReadProjectResponse>(output);
        }
    }
}