using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.SubArea;
using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interactors.SubArea
{
    public class UpdateSubArea : IUpdateSubArea
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IMapper _mapper;
        public UpdateSubArea(ISubAreaRepository subAreaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id, UpdateSubAreaInput input)
        {
            // Recupera entidade que será atualizada
            Entities.SubArea entity = await _subAreaRepository.GetById(id)
                ?? throw Validation.UseCaseException.NotFoundEntityById(nameof(Entities.SubArea));

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;
            entity.AreaId = input.AreaId;

            // Salva entidade atualizada no banco
            Entities.SubArea model = await _subAreaRepository.Update(entity);
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}