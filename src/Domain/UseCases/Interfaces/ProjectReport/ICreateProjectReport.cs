using Domain.Ports.ProjectReport;
using Domain.UseCases.Ports.ProjectReport;

namespace Domain.UseCases.Interfaces.ProjectReport
{
    public interface ICreateProjectReport
    {
        Task<DetailedReadProjectReportOutput> ExecuteAsync(CreateProjectReportInput input);
    }
}