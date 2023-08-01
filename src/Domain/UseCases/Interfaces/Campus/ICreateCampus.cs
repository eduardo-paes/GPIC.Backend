using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interfaces.Campus
{
    public interface ICreateCampus
    {
        Task<DetailedReadCampusOutput> Execute(CreateCampusInput model);
    }
}