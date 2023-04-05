using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases.Campus
{
    public interface IGetCampuses
    {
        Task<IQueryable<ResumedReadCampusOutput>> Execute(int skip, int take);
    }
}