using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IReportService
    {
        Task<string> GenerateCertificateAsync(Project project, string cordinatorName);
    }
}