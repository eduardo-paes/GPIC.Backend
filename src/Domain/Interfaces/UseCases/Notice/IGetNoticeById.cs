using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases.Notice
{
    public interface IGetNoticeById
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id);
    }
}