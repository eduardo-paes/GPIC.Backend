using Domain.Contracts.MainArea;

namespace Domain.Interfaces.MainArea
{
    public interface IGetMainAreas
    {
        Task<IQueryable<ResumedReadMainAreaOutput>> Execute(int skip, int take);
    }
}