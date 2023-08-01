using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interfaces.Notice
{
    public interface IUpdateNotice
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput model);
    }
}