using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface IDeleteSubArea
    {
        Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id);
    }
}