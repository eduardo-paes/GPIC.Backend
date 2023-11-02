using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Area;
using Application.Ports.Area;
using Application.Validation;

namespace Application.UseCases.Area
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

        public async Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id, UpdateAreaInput input)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.MainArea));

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;
            entity.MainAreaId = input.MainAreaId;

            // Salva entidade atualizada no banco
            var model = await _repository.UpdateAsync(entity);
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}