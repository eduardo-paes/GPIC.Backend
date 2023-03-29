using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases.Notice
{
    public interface IUpdateNotice
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput model);
    }
}