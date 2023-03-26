using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases.MainArea;
using AutoMapper;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.MainArea
{
    public class CreateMainArea : ICreateMainArea
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public CreateMainArea(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput dto)
        {
            // Validação de código da Área
            var entity = await _repository.GetByCode(dto.Code);
            if (entity != null)
                throw new Exception($"Já existe uma Área Principal para o código {dto.Code}");

            entity = await _repository.Create(_mapper.Map<Domain.Entities.MainArea>(dto));
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}