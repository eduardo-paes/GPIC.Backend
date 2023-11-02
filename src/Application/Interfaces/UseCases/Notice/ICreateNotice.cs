using Application.Ports.Notice;

namespace Application.Interfaces.UseCases.Notice
{
    public interface ICreateNotice
    {
        Task<DetailedReadNoticeOutput> ExecuteAsync(CreateNoticeInput model);
    }
}