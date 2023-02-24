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
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public MainAreaService(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MainAreaDTO> Create(MainAreaDTO dto)
        {
            var entity = await _repository.Create(_mapper.Map<MainArea>(dto));
            return _mapper.Map<MainAreaDTO>(entity);
        }

        public async Task<MainAreaDTO> Delete(Guid id)
        {
            var entity = await _repository.Delete(id);
            return _mapper.Map<MainAreaDTO>(entity);
        }

        public async Task<IQueryable<MainAreaDTO>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<MainAreaDTO>>(entities).AsQueryable();
        }

        public async Task<MainAreaDTO> GetById(Guid? id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<MainAreaDTO>(entity);
        }

        public async Task<MainAreaDTO> Update(Guid? id, MainAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<MainAreaDTO>(model);
        }
    }
}

