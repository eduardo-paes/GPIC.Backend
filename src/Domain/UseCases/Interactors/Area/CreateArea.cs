using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Ports.Area;
using Domain.Validation;

namespace Domain.UseCases
{
    public class CreateArea : ICreateArea
    {
        #region Global Scope
        private readonly IAreaRepository _areaRepository;
        private readonly IMainAreaRepository _mainAreaRepository;
        private readonly IMapper _mapper;
        public CreateArea(IAreaRepository areaRepository, IMainAreaRepository mainAreaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _mainAreaRepository = mainAreaRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadAreaOutput> Execute(CreateAreaInput input)
        {
            var entity = await _areaRepository.GetByCode(input.Code);
            UseCaseException.BusinessRuleViolation(entity != null, $"Já existe uma área principal para o código {input.Code}.");

            // Verifica id da área princial
            UseCaseException.NotInformedParam(input.MainAreaId == null, nameof(input.MainAreaId));

            // Valida se existe área principal
            var area = await _mainAreaRepository.GetById(input.MainAreaId);
            UseCaseException.BusinessRuleViolation(area?.DeletedAt != null, "A Área Principal informada está inativa.");

            // Mapeia input para entidade
            var newEntity = new Entities.Area(input.MainAreaId, input.Code, input.Name);

            // Cria nova área
            entity = await _areaRepository.Create(newEntity);
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}