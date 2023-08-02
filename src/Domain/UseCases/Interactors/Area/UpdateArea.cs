using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Ports.Area;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Area
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
        #endregion Global Scope

        public async Task<DetailedReadAreaOutput> Execute(Guid? id, UpdateAreaInput input)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Recupera entidade que será atualizada
            Entities.Area entity = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.MainArea));

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;
            entity.MainAreaId = input.MainAreaId;

            // Salva entidade atualizada no banco
            Entities.Area model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}