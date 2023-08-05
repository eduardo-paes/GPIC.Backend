using Application.Ports.Notice;

namespace Application.Interfaces.UseCases.Notice
{
    public interface IGetNotices
    {
        Task<IEnumerable<ResumedReadNoticeOutput>> ExecuteAsync(int skip, int take);
    }
}