using Application.Ports.Campus;

namespace Application.Interfaces.UseCases.Campus
{
    public interface ICreateCampus
    {
        Task<DetailedReadCampusOutput> ExecuteAsync(CreateCampusInput model);
    }
}