using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.Course;
using Application.Interfaces.UseCases.Course;
using Application.Validation;

namespace Application.UseCases.Course
{
    public class CreateCourse : ICreateCourse
    {
        #region Global Scope
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        public CreateCourse(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadCourseOutput> ExecuteAsync(CreateCourseInput model)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(model.Name), nameof(model.Name));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetCourseByNameAsync(model.Name!);
            UseCaseException.BusinessRuleViolation(entity != null, "Já existe um Curso para o nome informado.");

            // Cria entidade
            Domain.Entities.Course newEntity = new(model.Name);
            entity = await _repository.CreateAsync(newEntity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadCourseOutput>(entity);
        }
    }
}