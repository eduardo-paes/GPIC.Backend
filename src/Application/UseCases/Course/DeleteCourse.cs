using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Course;
using Application.Ports.Course;
using Application.Validation;

namespace Application.UseCases.Course
{
    public class DeleteCourse : IDeleteCourse
    {
        #region Global Scope
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        public DeleteCourse(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            var model = await _repository.DeleteAsync(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadCourseOutput>(model);
        }
    }
}