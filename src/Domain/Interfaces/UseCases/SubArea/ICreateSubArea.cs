using Domain.Contracts.SubArea;

namespace Domain.Interfaces.UseCases.SubArea
{
    public interface ICreateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(CreateSubAreaInput model);
    }
}