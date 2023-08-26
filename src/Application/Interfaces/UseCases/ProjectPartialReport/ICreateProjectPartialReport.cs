using Application.Ports.ProjectPartialReport;

namespace Application.Interfaces.UseCases.ProjectPartialReport
{
    public interface ICreateProjectPartialReport
    {
        Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(CreateProjectPartialReportInput input);
    }
}