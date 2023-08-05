using Domain.UseCases.Ports.ProjectReport;

namespace Domain.UseCases.Interfaces.ProjectReport
{
    public interface IDeleteProjectReport
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id);
    }
}