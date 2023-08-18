using Application.Ports.ProjectFinalReport;

namespace Application.Interfaces.UseCases.ProjectFinalReport
{
    public interface ICreateProjectFinalReport
    {
        Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(CreateProjectFinalReportInput input);
    }
}