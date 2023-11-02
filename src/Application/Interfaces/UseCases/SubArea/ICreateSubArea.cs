using Application.Ports.SubArea;

namespace Application.Interfaces.UseCases.SubArea
{
    public interface ICreateSubArea
    {
        Task<DetailedReadSubAreaOutput> ExecuteAsync(CreateSubAreaInput model);
    }
}