using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface IUpdateSubArea
    {
        Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id, UpdateSubAreaInput input);
    }
}