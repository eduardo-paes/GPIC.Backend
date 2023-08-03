using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface IGetCampuses
    {
        Task<IQueryable<ResumedReadCampusOutput>> ExecuteAsync(int skip, int take);
    }
}