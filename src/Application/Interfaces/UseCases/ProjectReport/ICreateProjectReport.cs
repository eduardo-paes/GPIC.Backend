using Application.Ports.ProjectReport;

namespace Application.Interfaces.UseCases.ProjectReport
{
    public interface ICreateProjectReport
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(CreateProjectReportInput input);
    }
}