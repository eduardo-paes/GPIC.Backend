using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interfaces.SubArea
{
    public interface IGetSubAreaById
    {
        Task<DetailedReadSubAreaOutput> Execute(Guid? id);
    }
}