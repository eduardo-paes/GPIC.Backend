using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Ports.Course;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Course
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
        #endregion Global Scope

        public async Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            Entities.Course? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadCourseOutput>(entity);
        }
    }
}