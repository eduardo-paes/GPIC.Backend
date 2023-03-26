using Domain.Contracts.SubArea;

namespace Domain.Interfaces.SubArea
{
    public interface IGetSubAreaById
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id);
    }
}