using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
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

        public async Task<DetailedReadCampusOutput> Execute(CreateCampusInput dto)
        {
            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetCampusByName(dto.Name);
            if (entity != null)
                throw new Exception($"Já existe um Campus para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.Campus>(dto));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadCampusOutput>(entity);
        }
    }
}