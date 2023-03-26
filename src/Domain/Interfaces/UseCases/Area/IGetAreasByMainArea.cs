using Domain.Contracts.Area;

namespace Domain.Interfaces.Area
{
    public interface IGetAreasByMainArea
    {
        Task<IQueryable<ResumedReadAreaOutput>> Execute(Guid? mainAreaId, int skip, int take);
    }
}