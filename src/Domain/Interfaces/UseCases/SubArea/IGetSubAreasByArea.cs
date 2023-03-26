using Domain.Contracts.SubArea;

namespace Domain.Interfaces.SubArea
{
    public interface IGetSubAreasByArea
    {
        Task<IQueryable<ResumedReadSubAreaOutput>> Execute(Guid? areaId, int skip, int take);
    }
}