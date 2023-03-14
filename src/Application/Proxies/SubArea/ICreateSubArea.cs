using Application.DTOs.SubArea;

namespace Application.Proxies.SubArea
{
    public interface ICreateSubArea
    {
        Task<DetailedReadSubAreaDTO> Execute(CreateSubAreaDTO model);
    }
}