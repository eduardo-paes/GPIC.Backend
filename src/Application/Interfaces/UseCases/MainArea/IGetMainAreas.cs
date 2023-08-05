using Application.Ports.MainArea;

namespace Application.Interfaces.UseCases.MainArea
{
    public interface IGetMainAreas
    {
        Task<IQueryable<ResumedReadMainAreaOutput>> ExecuteAsync(int skip, int take);
    }
}