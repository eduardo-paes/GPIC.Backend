using Domain.Contracts.Campus;

namespace Domain.Interfaces.UseCases.Campus
{
    public interface ICreateCampus
    {
        Task<DetailedReadCampusOutput> Execute(CreateCampusInput model);
    }
}