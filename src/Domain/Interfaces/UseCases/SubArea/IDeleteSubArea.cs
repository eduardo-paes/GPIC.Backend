using Domain.Contracts.SubArea;

namespace Domain.Interfaces.UseCases.SubArea
{
    public interface IDeleteSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id);
    }
}