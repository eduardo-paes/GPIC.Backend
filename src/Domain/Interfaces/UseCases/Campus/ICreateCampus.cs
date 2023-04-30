using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateCampus
    {
        Task<DetailedReadCampusOutput> Execute(CreateCampusInput model);
    }
}