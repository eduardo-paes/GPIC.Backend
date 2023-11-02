using Application.Ports.SubArea;

namespace Application.Interfaces.UseCases.SubArea
{
    public interface IDeleteSubArea
    {
        Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id);
    }
}