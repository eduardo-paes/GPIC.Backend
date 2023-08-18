using Application.Ports.ProjectFinalReport;

namespace Application.Interfaces.UseCases.ProjectFinalReport
{
    public interface IDeleteProjectFinalReport
    {
        Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? id);
    }
}