using Application.Ports.ProjectPartialReport;

namespace Application.Interfaces.UseCases.ProjectPartialReport
{
    public interface IDeleteProjectPartialReport
    {
        Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(Guid? id);
    }
}