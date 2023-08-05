using Application.Ports.Area;

namespace Application.Interfaces.UseCases.Area
{
    public interface IGetAreasByMainArea
    {
        Task<IQueryable<ResumedReadAreaOutput>> ExecuteAsync(Guid? mainAreaId, int skip, int take);
    }
}