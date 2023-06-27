using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System;

namespace Domain.UseCases
{
    public class UpdateMainArea : IUpdateMainArea
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public UpdateMainArea(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedMainAreaOutput> Execute(Guid? id, UpdateMainAreaInput input)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id) ?? throw new Exception("Área Principal não encontrada.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedMainAreaOutput>(model);
        }
    }
}