using Domain.UseCases.Ports.Notice;

namespace Domain.UseCases.Interfaces.Notice
{
    public interface ICreateNotice
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(CreateNoticeInput model);
    }
}