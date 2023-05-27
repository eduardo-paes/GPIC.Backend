using Domain.Contracts.Area;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System;

namespace Domain.UseCases
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

        public async Task<DetailedReadAreaOutput> Execute(Guid? id, UpdateAreaInput input)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se entidade existe
            if (entity == null)
                throw new Exception("Área não encontrada.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;
            entity.MainAreaId = input.MainAreaId;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}