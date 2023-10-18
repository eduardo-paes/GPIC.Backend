using Application.Ports.ProjectActivity;

namespace Application.Interfaces.UseCases.Project
{
    public interface IGetActivitiesByProjectId
    {
        Task<IEnumerable<DetailedReadProjectActivityOutput>> ExecuteAsync(Guid? projectId);
    }
}