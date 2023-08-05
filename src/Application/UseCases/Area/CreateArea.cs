using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Area;
using Application.Ports.Area;
using Application.Validation;

namespace Application.UseCases.Area
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

        public async Task<DetailedReadAreaOutput> ExecuteAsync(CreateAreaInput input)
        {
            var entity = await _areaRepository.GetByCodeAsync(input.Code);
            UseCaseException.BusinessRuleViolation(entity != null, $"Já existe uma área principal para o código {input.Code}.");

            // Verifica id da área princial
            UseCaseException.NotInformedParam(input.MainAreaId == null, nameof(input.MainAreaId));

            // Valida se existe área principal
            var area = await _mainAreaRepository.GetByIdAsync(input.MainAreaId);
            UseCaseException.BusinessRuleViolation(area?.DeletedAt != null, "A Área Principal informada está inativa.");

            // Mapeia input para entidade
            var newEntity = new Domain.Entities.Area(input.MainAreaId, input.Code, input.Name);

            // Cria nova área
            entity = await _areaRepository.CreateAsync(newEntity);
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}