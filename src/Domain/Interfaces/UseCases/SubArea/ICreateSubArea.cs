using Domain.Contracts.SubArea;

namespace Domain.Interfaces.SubArea
{
    public interface ICreateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(CreateSubAreaInput model);
    }
}