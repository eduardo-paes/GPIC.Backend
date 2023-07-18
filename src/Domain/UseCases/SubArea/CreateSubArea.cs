using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class CreateSubArea : ICreateSubArea
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public CreateSubArea(ISubAreaRepository subAreaRepository, IAreaRepository areaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadSubAreaOutput> Execute(CreateSubAreaInput input)
        {
            var entity = await _subAreaRepository.GetByCode(input.Code);
            UseCaseException.BusinessRuleViolation(entity != null,
                "Já existe uma Subárea para o código informado.");

            // Verifica id da área
            UseCaseException.NotInformedParam(input.AreaId == null, nameof(input.AreaId));

            // Valida se existe área
            var area = await _areaRepository.GetById(input.AreaId)
                ?? throw UseCaseException.NotFoundEntityByParams(nameof(Entities.Area));

            // Verifica se área está ativa
            UseCaseException.BusinessRuleViolation(area.DeletedAt != null,
                "A Área informada está inativa.");

            // Cria nova área
            entity = await _subAreaRepository.Create(_mapper.Map<Domain.Entities.SubArea>(input));
            return _mapper.Map<DetailedReadSubAreaOutput>(entity);
        }
    }
}