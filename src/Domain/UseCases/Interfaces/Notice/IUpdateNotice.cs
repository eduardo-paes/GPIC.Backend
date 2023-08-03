using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interfaces.Notice
{
    public interface IUpdateNotice
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id, UpdateNoticeInput model);
    }
}