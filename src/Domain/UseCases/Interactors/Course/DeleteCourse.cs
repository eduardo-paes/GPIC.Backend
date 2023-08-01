using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Ports.Course;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Course
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

        public async Task<DetailedReadCourseOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.Course model = await _repository.Delete(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadCourseOutput>(model);
        }
    }
}