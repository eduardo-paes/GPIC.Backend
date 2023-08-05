using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.SubArea;
using Application.Ports.SubArea;
using Application.Validation;

namespace Application.UseCases.SubArea
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
            Domain.Entities.SubArea entity = await _subAreaRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.SubArea));

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;
            entity.AreaId = input.AreaId;

            // Salva entidade atualizada no banco
            Domain.Entities.SubArea model = await _subAreaRepository.UpdateAsync(entity);
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}