using Domain.Contracts.Area;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Area;

namespace Domain.UseCases.Area
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
            if (entity != null)
                throw new Exception($"Já existe uma Área Principal para o código {model.Code}");

            // Verifica id da área princial
            if (model.MainAreaId == null)
                throw new Exception($"O Id da Área Principal não pode ser vazio.");

            // Valida se existe área principal
            var area = await _mainAreaRepository.GetById(model.MainAreaId);
            if (area.DeletedAt != null)
                throw new Exception($"A Área Principal informada está inativa.");

            // Cria nova área
            entity = await _areaRepository.Create(_mapper.Map<Domain.Entities.Area>(model));
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}