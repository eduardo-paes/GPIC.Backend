using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interfaces.Notice
{
    public interface IDeleteNotice
    {
        Task<DetailedReadNoticeOutput> Execute(Guid? id);
    }
}