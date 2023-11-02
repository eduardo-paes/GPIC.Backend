using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface INoticeRepository : IGenericCRUDRepository<Notice>
    {
        Task<Notice?> GetNoticeByPeriodAsync(DateTime start, DateTime end);

        /// <summary>
        /// Busca edital que possui data final de entrega de relatório para o dia anterior.
        /// </summary>
        /// <returns>Edital que possui data final de entrega de relatório para o dia anterior.</returns>
        Task<Notice?> GetNoticeEndingAsync();
    }
}