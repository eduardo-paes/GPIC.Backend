using Application.Ports.ProjectPartialReport;

namespace Application.Interfaces.UseCases.ProjectPartialReport
{
    public interface IGetProjectPartialReportById
    {
        Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(Guid? id);
    }
}