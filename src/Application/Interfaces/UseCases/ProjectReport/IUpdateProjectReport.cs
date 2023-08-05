using Application.Ports.ProjectReport;

namespace Application.Interfaces.UseCases.ProjectReport
{
    public interface IUpdateProjectReport
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id, UpdateProjectReportInput input);
    }
}