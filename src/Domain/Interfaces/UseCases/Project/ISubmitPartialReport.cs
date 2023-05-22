using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface ISubmitPartialReport
    {
        Task<ProjectReportOutput> Execute(ProjectReportInput input);
    }
}