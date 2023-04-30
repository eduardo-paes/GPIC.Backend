using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases
{
    public interface IDeleteNotice
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id);
    }
}