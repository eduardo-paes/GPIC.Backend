using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface ICourseRepository : IGenericCRUDRepository<Course>
    {
        Task<Course?> GetCourseByNameAsync(string name);
    }
}