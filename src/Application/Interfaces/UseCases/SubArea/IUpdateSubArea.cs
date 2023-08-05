using Application.Ports.SubArea;

namespace Application.Interfaces.UseCases.SubArea
{
    public interface IUpdateSubArea
    {
        Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id, UpdateSubAreaInput input);
    }
}