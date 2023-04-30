using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateNotice
    {
        Task<DetailedReadNoticeOutput> Execute(CreateNoticeInput model);
    }
}