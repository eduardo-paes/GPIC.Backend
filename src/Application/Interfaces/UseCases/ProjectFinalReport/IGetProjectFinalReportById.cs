using Application.Ports.ProjectFinalReport;

namespace Application.Interfaces.UseCases.ProjectFinalReport
{
    public interface IGetProjectFinalReportById
    {
        Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? id);
    }
}