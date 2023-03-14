using Application.DTOs.SubArea;
using Application.Proxies.SubArea;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.SubArea
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

        public async Task<DetailedReadSubAreaDTO> Execute(CreateSubAreaDTO dto)
        {
            var entity = await _subAreaRepository.GetByCode(dto.Code);
            if (entity != null)
                throw new Exception($"Já existe uma Sub Área para o código {dto.Code}");

            // Verifica id da área
            if (dto.AreaId == null)
                throw new Exception($"O Id da Área não pode ser vazio.");

            // Valida se existe área
            var area = await _areaRepository.GetById(dto.AreaId);
            if (area.DeletedAt != null)
                throw new Exception($"A Área informada está inativa.");

            // Cria nova área
            entity = await _subAreaRepository.Create(_mapper.Map<Domain.Entities.SubArea>(dto));
            return _mapper.Map<DetailedReadSubAreaDTO>(entity);
        }
    }
}