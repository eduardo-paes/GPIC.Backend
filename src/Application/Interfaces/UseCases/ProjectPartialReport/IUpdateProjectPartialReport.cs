using Application.Ports.ProjectPartialReport;

namespace Application.Interfaces.UseCases.ProjectPartialReport
{
    public interface IUpdateProjectPartialReport
    {
        Task<DetailedReadProjectPartialReportOutput> ExecuteAsync(Guid? id, UpdateProjectPartialReportInput input);
    }
}