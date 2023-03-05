using System;
using AutoMapper;
using CopetSystem.Application.DTOs.Area;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;

namespace CopetSystem.Application.Services
{
    public class AreaService : IAreaService
    {
        #region Global Scope
        private readonly IAreaRepository _areaRepository;
        private readonly IMainAreaRepository _mainAreaRepository;
        private readonly IMapper _mapper;
        public AreaService(IAreaRepository areaRepository, IMainAreaRepository mainAreaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _mainAreaRepository = mainAreaRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<ReadAreaDTO> Create(CreateAreaDTO dto)
        {
            var entity = await _areaRepository.GetByCode(dto.Code);
            if (entity != null)
                throw new Exception($"Já existe uma Principal para o código {dto.Code}");

            // Verifica id da área princial
            if (dto.MainAreaId == null)
                throw new Exception($"O Id da Área Principal não pode ser vazio.");

            // Valida se existe área principal
            var area = await _mainAreaRepository.GetById(dto.MainAreaId);

            // Cria nova área
            entity = await _areaRepository.Create(_mapper.Map<Area>(dto));
            return _mapper.Map<ReadAreaDTO>(entity);
        }

        public async Task<ReadAreaDTO> Delete(Guid id)
        {
            var model = await _areaRepository.Delete(id);
            return _mapper.Map<ReadAreaDTO>(model);
        }

        public async Task<IQueryable<ReadAreaDTO>> GetAll()
        {
            var entities = await _areaRepository.GetAll();
            return _mapper.Map<IEnumerable<ReadAreaDTO>>(entities).AsQueryable();
        }

        public async Task<ReadAreaDTO> GetById(Guid? id)
        {
            var entity = await _areaRepository.GetById(id);
            return _mapper.Map<ReadAreaDTO>(entity);
        }

        public async Task<ReadAreaDTO> Update(Guid? id, UpdateAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _areaRepository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);
            entity.UpdateMainArea(dto.MainAreaId);

            // Salva entidade atualizada no banco
            var model = await _areaRepository.Update(entity);
            return _mapper.Map<ReadAreaDTO>(model);
        }
        #endregion
    }
}

