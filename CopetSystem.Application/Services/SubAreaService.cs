using System;
using AutoMapper;
using CopetSystem.Application.DTOs.SubArea;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;

namespace CopetSystem.Application.Services
{
    public class SubAreaService : ISubAreaService
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public SubAreaService(ISubAreaRepository subAreaRepository, IAreaRepository areaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<DetailedReadSubAreaDTO> Create(CreateSubAreaDTO dto)
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
            entity = await _subAreaRepository.Create(_mapper.Map<SubArea>(dto));
            return _mapper.Map<DetailedReadSubAreaDTO>(entity);
        }

        public async Task<DetailedReadSubAreaDTO> Delete(Guid id)
        {
            var model = await _subAreaRepository.Delete(id);
            return _mapper.Map<DetailedReadSubAreaDTO>(model);
        }

        public async Task<IQueryable<ResumedReadSubAreaDTO>> GetSubAreasByArea(Guid? areaId, int skip, int take)
        {
            var entities = await _subAreaRepository.GetSubAreasByArea(areaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadSubAreaDTO>>(entities).AsQueryable();
        }

        public async Task<DetailedReadSubAreaDTO> GetById(Guid? id)
        {
            var entity = await _subAreaRepository.GetById(id);
            return _mapper.Map<DetailedReadSubAreaDTO>(entity);
        }

        public async Task<DetailedReadSubAreaDTO> Update(Guid? id, UpdateSubAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _subAreaRepository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);
            entity.UpdateArea(dto.AreaId);

            // Salva entidade atualizada no banco
            var model = await _subAreaRepository.Update(entity);
            return _mapper.Map<DetailedReadSubAreaDTO>(model);
        }
        #endregion
    }
}

