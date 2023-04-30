using Domain.Contracts.Course;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadCourseOutput> Execute(CreateCourseInput dto)
        {
            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetCourseByName(dto.Name);
            if (entity != null)
                throw new Exception($"Já existe um Curso para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.Course>(dto));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadCourseOutput>(entity);
        }
    }
}