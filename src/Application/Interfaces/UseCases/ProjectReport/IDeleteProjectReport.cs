using Application.Ports.ProjectReport;

namespace Application.Interfaces.UseCases.ProjectReport
{
    public interface IDeleteProjectReport
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id);
    }
}