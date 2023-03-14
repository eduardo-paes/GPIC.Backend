using Application.DTOs.SubArea;

namespace Application.Proxies.SubArea
{
    public interface IDeleteSubArea
    {
        Task<DetailedReadSubAreaDTO> Execute(Guid id);
    }
}