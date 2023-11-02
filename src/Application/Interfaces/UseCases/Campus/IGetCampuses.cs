using Application.Ports.Campus;

namespace Application.Interfaces.UseCases.Campus
{
    public interface IGetCampuses
    {
        Task<IQueryable<ResumedReadCampusOutput>> ExecuteAsync(int skip, int take);
    }
}