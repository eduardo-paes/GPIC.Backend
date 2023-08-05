using Application.Ports.ProjectReport;

namespace Application.Interfaces.UseCases.ProjectReport
{
    public interface IGetProjectReportById
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id);
    }
}