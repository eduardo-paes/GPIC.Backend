using Domain.Contracts.SubArea;

namespace Domain.Interfaces.SubArea
{
    public interface IUpdateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id, UpdateSubAreaOutput model);
    }
}