using Application.Ports.SubArea;

namespace Application.Interfaces.UseCases.SubArea
{
    public interface IGetSubAreaById
    {
        Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id);
    }
}