using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases
{
    public interface IGetNoticeById
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id);
    }
}