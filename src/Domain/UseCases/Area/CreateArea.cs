using Domain.Contracts.Area;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
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

        public async Task<DetailedReadAreaOutput> Execute(CreateAreaInput model)
        {
            var entity = await _areaRepository.GetByCode(model.Code);
            UseCaseException.BusinessRuleViolation(entity != null, $"There is already a Main Area for the code {model.Code}.");

            // Verifica id da área princial
            UseCaseException.NotInformedParam(model.MainAreaId == null, nameof(model.MainAreaId));

            // Valida se existe área principal
            var area = await _mainAreaRepository.GetById(model.MainAreaId);
            UseCaseException.BusinessRuleViolation(area?.DeletedAt != null, "The informed Main Area is inactive.");

            // Cria nova área
            entity = await _areaRepository.Create(_mapper.Map<Domain.Entities.Area>(model));
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}