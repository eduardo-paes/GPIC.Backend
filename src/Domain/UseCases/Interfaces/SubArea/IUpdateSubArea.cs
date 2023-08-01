using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface IUpdateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id, UpdateSubAreaInput input);
    }
}