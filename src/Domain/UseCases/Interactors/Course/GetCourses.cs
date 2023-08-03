using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Ports.Course;

namespace Domain.UseCases.Interactors.Course
{
    public class GetCourses : IGetCourses
    {
        #region Global Scope
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        public GetCourses(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadCourseOutput>> ExecuteAsync(int skip, int take)
        {
            IEnumerable<Entities.Course> entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCourseOutput>>(entities).AsQueryable();
        }
    }
}