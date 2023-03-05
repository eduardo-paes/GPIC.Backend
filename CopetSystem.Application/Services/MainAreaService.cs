using System;
using AutoMapper;
using CopetSystem.Application.DTOs.MainArea;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;

namespace CopetSystem.Application.Services
{
	public class MainAreaService : IMainAreaService
	{
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public MainAreaService(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<ReadMainAreaDTO> Create(CreateMainAreaDTO dto)
        {
            // Validação de código da Área
            var entity = await _repository.GetByCode(dto.Code);
            if (entity != null)
                throw new Exception($"Já existe uma Área Principal para o código {dto.Code}");

            entity = await _repository.Create(_mapper.Map<MainArea>(dto));
            return _mapper.Map<ReadMainAreaDTO>(entity);
        }

        public async Task<ReadMainAreaDTO> Delete(Guid id)
        {
            var model = await _repository.Delete(id);
            return _mapper.Map<ReadMainAreaDTO>(model);
        }

        public async Task<IQueryable<ReadMainAreaDTO>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<ReadMainAreaDTO>>(entities).AsQueryable();
        }

        public async Task<ReadMainAreaDTO> GetById(Guid? id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<ReadMainAreaDTO>(entity);
        }

        public async Task<ReadMainAreaDTO> Update(Guid? id, UpdateMainAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<ReadMainAreaDTO>(model);
        }
        #endregion
    }
}

