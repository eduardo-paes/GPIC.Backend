using Application.DTOs.SubArea;

namespace Application.Proxies.SubArea
{
    public interface IUpdateSubArea
    {
        Task<DetailedReadSubAreaDTO> Execute(Guid? id, UpdateSubAreaDTO model);
    }
}