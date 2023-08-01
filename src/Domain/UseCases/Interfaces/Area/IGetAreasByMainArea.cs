using Domain.UseCases.Ports.Area;

namespace Domain.UseCases.Interfaces.Area
{
    public interface IGetAreasByMainArea
    {
        Task<IQueryable<ResumedReadAreaOutput>> Execute(Guid? mainAreaId, int skip, int take);
    }
}