using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateNotice
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput model);
    }
}