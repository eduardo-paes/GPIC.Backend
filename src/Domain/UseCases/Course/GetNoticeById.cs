using Domain.Contracts.Course;
using Domain.Interfaces.UseCases.Course;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Course
{
    public class GetCourseById : IGetCourseById
    {
        #region Global Scope
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        public GetCourseById(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadCourseOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadCourseOutput>(entity);
        }
    }
}