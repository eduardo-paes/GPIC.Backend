using Application.Proxies.Area;
using Application.DTOs.Area;
using AutoMapper;
using Domain.Interfaces;

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

        public async Task<DetailedReadAreaDTO> Execute(CreateAreaDTO dto)
        {
            var entity = await _areaRepository.GetByCode(dto.Code);
            if (entity != null)
                throw new Exception($"Já existe uma Principal para o código {dto.Code}");

            // Verifica id da área princial
            if (dto.MainAreaId == null)
                throw new Exception($"O Id da Área Principal não pode ser vazio.");

            // Valida se existe área principal
            var area = await _mainAreaRepository.GetById(dto.MainAreaId);
            if (area.DeletedAt != null)
                throw new Exception($"A Área Principal informada está inativa.");

            // Cria nova área
            entity = await _areaRepository.Create(_mapper.Map<Domain.Entities.Area>(dto));
            return _mapper.Map<DetailedReadAreaDTO>(entity);
        }
    }
}