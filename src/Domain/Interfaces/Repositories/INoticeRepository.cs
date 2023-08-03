using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface INoticeRepository : IGenericCRUDRepository<Notice>
    {
        Task<Notice?> GetNoticeByPeriodAsync(DateTime start, DateTime end);
    }
}