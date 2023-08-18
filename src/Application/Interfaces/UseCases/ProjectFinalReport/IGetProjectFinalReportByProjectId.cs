using Application.Ports.ProjectFinalReport;

namespace Application.Interfaces.UseCases.ProjectFinalReport
{
    public interface IGetProjectFinalReportsByProjectId
    {
        Task<IList<DetailedReadProjectFinalReportOutput>> ExecuteAsync(Guid? projectId);
    }
}