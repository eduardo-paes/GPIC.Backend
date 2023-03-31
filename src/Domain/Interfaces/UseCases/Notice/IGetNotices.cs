using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases.Notice
{
    public interface IGetNotices
    {
        Task<IQueryable<ResumedReadNoticeOutput>> Execute(int skip, int take);
    }
}