using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases.Notice
{
    public interface IDeleteNotice
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id);
    }
}