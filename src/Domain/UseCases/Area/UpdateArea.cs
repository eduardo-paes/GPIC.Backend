using Domain.Contracts.Area;
using Domain.Interfaces.UseCases.Area;
using AutoMapper;
using Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System;

namespace Domain.UseCases.Area
{
    public class UpdateArea : IUpdateArea
    {
        #region Global Scope
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        public UpdateArea(IAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadAreaOutput> Execute(Guid? id, UpdateAreaInput dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Atualiza atributos permitidos
            entity.Name = dto.Name;
            entity.Code = dto.Code;
            entity.MainAreaId = dto.MainAreaId;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}