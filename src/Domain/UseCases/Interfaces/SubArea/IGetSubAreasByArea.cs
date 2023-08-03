using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface IGetSubAreasByArea
    {
        Task<IQueryable<ResumedReadSubAreaOutput>> ExecuteAsync(Guid? areaId, int skip, int take);
    }
}