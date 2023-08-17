using Application.Ports.ProjectFinalReport;

namespace Application.Interfaces.UseCases.ProjectFinalReport
{
    public interface IUpdateProjectFinalReport
    {
        Task<DetailedReadProjectFinalReportOutput> ExecuteAsync(Guid? id, UpdateProjectFinalReportInput input);
    }
}