using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Campus;
using Application.Ports.Campus;
using Application.Validation;

namespace Application.UseCases.Campus
{
    public class CreateCampus : ICreateCampus
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public CreateCampus(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadCampusOutput> ExecuteAsync(CreateCampusInput input)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetCampusByNameAsync(input.Name!);
            if (entity != null)
                throw UseCaseException.BusinessRuleViolation("Já existe um Campus para o nome informado.");

            // Cria entidade
            var newEntity = new Domain.Entities.Campus(input.Name);
            entity = await _repository.CreateAsync(newEntity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadCampusOutput>(entity);
        }
    }
}