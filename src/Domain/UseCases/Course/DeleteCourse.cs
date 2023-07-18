using Domain.Contracts.Course;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadCourseOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadCourseOutput>(model);
        }
    }
}