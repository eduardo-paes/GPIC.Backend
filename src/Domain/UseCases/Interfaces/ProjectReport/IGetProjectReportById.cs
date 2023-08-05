using Domain.UseCases.Ports.ProjectReport;

namespace Domain.UseCases.Interfaces.ProjectReport
{
    public interface IGetProjectReportById
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id);
    }
}