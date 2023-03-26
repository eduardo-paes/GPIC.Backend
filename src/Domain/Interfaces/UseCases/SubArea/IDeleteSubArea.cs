using Domain.Contracts.SubArea;

namespace Domain.Interfaces.SubArea
{
    public interface IDeleteSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid id);
    }
}