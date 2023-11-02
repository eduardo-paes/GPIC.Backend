using Application.Ports.Notice;

namespace Application.Interfaces.UseCases.Notice
{
    public interface IGetNoticeById
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id);
    }
}