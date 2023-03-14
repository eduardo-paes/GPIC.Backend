using Application.DTOs.SubArea;
using Application.Proxies.SubArea;
using AutoMapper;
using Domain.Interfaces;

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
        #endregion

        public async Task<DetailedReadSubAreaDTO> Execute(Guid? id, UpdateSubAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _subAreaRepository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);
            entity.UpdateArea(dto.AreaId);

            // Salva entidade atualizada no banco
            var model = await _subAreaRepository.Update(entity);
            return _mapper.Map<DetailedReadSubAreaDTO>(model);
        }
    }
}