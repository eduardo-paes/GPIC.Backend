using Domain.Contracts.Course;
using Domain.Interfaces.UseCases.Course;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Course
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
        #endregion

        public async Task<DetailedReadCourseOutput> Execute(Guid? id, UpdateCourseInput dto)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se entidade existe
            if (entity == null)
                throw new Exception("Curso não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Curso informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, dto.Name, StringComparison.OrdinalIgnoreCase) && await _repository.GetCourseByName(dto.Name) != null)
                throw new Exception("Já existe um Curso para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = dto.Name;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadCourseOutput>(model);
        }
    }
}