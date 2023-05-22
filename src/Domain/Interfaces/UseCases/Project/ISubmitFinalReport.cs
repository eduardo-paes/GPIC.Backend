using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface ISubmitFinalReport
    {
        Task<ProjectReportOutput> Execute(ProjectReportInput input);
    }
}