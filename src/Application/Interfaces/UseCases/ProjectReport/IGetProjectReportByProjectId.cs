using Application.Ports.ProjectReport;

namespace Application.Interfaces.UseCases.ProjectReport
{
    public interface IGetProjectReportsByProjectId
    {
        Task<IList<DetailedReadProjectReportOutput>> ExecuteAsync(Guid? projectId);
    }
}