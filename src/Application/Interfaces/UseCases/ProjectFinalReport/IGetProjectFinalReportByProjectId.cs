using Application.Ports.ProjectFinalReport;

namespace Application.Interfaces.UseCases.ProjectFinalReport
{
    public interface IGetProjectFinalReportByProjectId
    {
        Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? projectId);
    }
}