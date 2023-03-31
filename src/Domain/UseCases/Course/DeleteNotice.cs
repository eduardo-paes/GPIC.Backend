using Domain.Contracts.Course;
using Domain.Interfaces.UseCases.Course;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Course
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
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se o edital existe
            var model = await _repository.Delete(id);
            if (model == null)
                throw new Exception("Curso não encontrado.");

            // Retorna o edital removido
            return _mapper.Map<DetailedReadCourseOutput>(model);
        }
    }
}