using Domain.Contracts.SubArea;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(CreateSubAreaInput model);
    }
}