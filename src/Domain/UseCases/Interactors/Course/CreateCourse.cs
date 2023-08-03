using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Ports.Course;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Ports.Course;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Course
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
            Entities.Course? entity = await _repository.GetCourseByNameAsync(model.Name!);
            if (entity != null)
            {
                throw UseCaseException.BusinessRuleViolation("Já existe um Curso para o nome informado.");
            }

            // Cria entidade
            Entities.Course newEntity = new(model.Name);
            entity = await _repository.CreateAsync(newEntity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadCourseOutput>(entity);
        }
    }
}