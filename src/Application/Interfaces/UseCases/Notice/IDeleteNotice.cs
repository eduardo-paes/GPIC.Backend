using Application.Ports.Notice;

namespace Application.Interfaces.UseCases.Notice
{
    public interface IDeleteNotice
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id);
    }
}