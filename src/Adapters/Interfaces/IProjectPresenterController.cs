using Adapters.Gateways.Project;

namespace Adapters.Interfaces;
public interface IProjectPresenterController
{
    Task<IList<ResumedReadProjectResponse>> GetClosedProjects(int skip, int take, bool onlyMyProjects = true);
    Task<IList<ResumedReadProjectResponse>> GetOpenProjects(int skip, int take, bool onlyMyProjects = true);
    Task<DetailedReadProjectResponse> GetProjectById(Guid? id);
    Task<ResumedReadProjectResponse> OpenProject(OpenProjectRequest input);
    Task<ResumedReadProjectResponse> UpdateProject(UpdateProjectRequest input);
    Task<ResumedReadProjectResponse> CancelProject(Guid? id, string? observation);

    // Task<ProjectDocumentsOutput> SubmitDocuments(ProjectDocumentsInput input);
    // Task<ProjectReportOutput> SubmitFinalReport(ProjectReportInput input);
    // Task<ProjectReportOutput> SubmitPartialReport(ProjectReportInput input);

    Task<ResumedReadProjectResponse> AppealProject(Guid? projectId, string? appealDescription);
    Task<ResumedReadProjectResponse> SubmitProject(Guid? projectId);
}