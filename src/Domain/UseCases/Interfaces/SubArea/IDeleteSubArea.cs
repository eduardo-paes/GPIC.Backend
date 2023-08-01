using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface IDeleteSubArea
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id);
    }
}