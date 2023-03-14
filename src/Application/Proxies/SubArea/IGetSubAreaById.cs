using Application.DTOs.SubArea;

namespace Application.Proxies.SubArea
{
    public interface IGetSubAreaById
    {
        Task<DetailedReadSubAreaDTO> Execute(Guid? id);
    }
}