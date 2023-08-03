using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interfaces.Notice
{
    public interface IGetNoticeById
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id);
    }
}