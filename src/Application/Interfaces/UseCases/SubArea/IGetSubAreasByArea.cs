using Application.Ports.SubArea;

namespace Application.Interfaces.UseCases.SubArea
{
    public interface IGetSubAreasByArea
    {
        Task<IQueryable<ResumedReadSubAreaOutput>> ExecuteAsync(Guid? areaId, int skip, int take);
    }
}