using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases
{
    public interface IGetNotices
    {
        Task<IEnumerable<ResumedReadNoticeOutput>> Execute(int skip, int take);
    }
}