using Application.Ports.Notice;

namespace Application.Interfaces.UseCases.Notice
{
    public interface IUpdateNotice
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id, UpdateNoticeInput model);
    }
}