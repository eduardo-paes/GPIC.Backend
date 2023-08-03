using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interfaces.MainArea
{
    public interface IGetMainAreas
    {
        Task<IQueryable<ResumedReadMainAreaOutput>> ExecuteAsync(int skip, int take);
    }
}