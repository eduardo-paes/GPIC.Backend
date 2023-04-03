using Domain.Contracts.Course;
using Domain.Interfaces.UseCases.Course;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Course
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
        #endregion

        public async Task<IQueryable<ResumedReadCourseOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCourseOutput>>(entities).AsQueryable();
        }
    }
}