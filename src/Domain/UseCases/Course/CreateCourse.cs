using Domain.Contracts.Course;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

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

        public async Task<DetailedReadCourseOutput> Execute(CreateCourseInput input)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetCourseByName(input.Name!);
            if (entity != null)
                throw new Exception("Já existe um Curso para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.Course>(input));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadCourseOutput>(entity);
        }
    }
}