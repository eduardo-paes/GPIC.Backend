using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface ICreateCampus
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(CreateCampusInput model);
    }
}