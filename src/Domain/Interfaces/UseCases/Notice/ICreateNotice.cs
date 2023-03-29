using Domain.Contracts.Notice;

namespace Domain.Interfaces.UseCases.Notice
{
    public interface ICreateNotice
    {
        Task<DetailedReadNoticeOutput> Execute(CreateNoticeInput model);
    }
}