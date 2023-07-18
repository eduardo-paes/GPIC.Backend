using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadSubAreaOutput> Execute(Guid? id, UpdateSubAreaInput input)
        {
            // Recupera entidade que será atualizada
            var entity = await _subAreaRepository.GetById(id)
                ?? throw Validation.UseCaseException.NotFoundEntityById(nameof(Entities.SubArea));

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;
            entity.AreaId = input.AreaId;

            // Salva entidade atualizada no banco
            var model = await _subAreaRepository.Update(entity);
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}