using Application.Ports.ProjectPartialReport;

namespace Application.Interfaces.UseCases.ProjectPartialReport
{
    public interface IGetProjectPartialReportByProjectId
    {
        Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(Guid? projectId);
    }
}