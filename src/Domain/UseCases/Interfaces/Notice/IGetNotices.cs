using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interfaces.Notice
{
    public interface IGetNotices
    {
        Task<IEnumerable<ResumedReadNoticeOutput>> ExecuteAsync(int skip, int take);
    }
}