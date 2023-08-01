using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface ICreateSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(CreateSubAreaInput model);
    }
}