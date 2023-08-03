using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Ports.Course;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Course
{
    public class UpdateCourse : IUpdateCourse
    {
        #region Global Scope
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        public UpdateCourse(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadCourseOutput> ExecuteAsync(Guid? id, UpdateCourseInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            Entities.Course entity = await _repository.GetById(id) ??
                throw UseCaseException.NotFoundEntityById(nameof(Entities.Course));

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
            {
                throw UseCaseException.BusinessRuleViolation("O Curso informado já foi excluído.");
            }

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase) && await _repository.GetCourseByName(input.Name!) != null)
            {
                throw UseCaseException.BusinessRuleViolation("Já existe um Curso para o nome informado.");
            }

            // Atualiza atributos permitidos
            entity.Name = input.Name;

            // Salva entidade atualizada no banco
            Entities.Course model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadCourseOutput>(model);
        }
    }
}