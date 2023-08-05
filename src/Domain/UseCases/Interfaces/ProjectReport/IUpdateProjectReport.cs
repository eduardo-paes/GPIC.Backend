using Domain.UseCases.Ports.ProjectReport;

namespace Domain.UseCases.Interfaces.ProjectReport
{
    public interface IUpdateProjectReport
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id, UpdateProjectReportInput input);
    }
}