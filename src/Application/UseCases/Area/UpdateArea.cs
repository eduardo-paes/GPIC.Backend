using Application.DTOs.Area;
using Application.Proxies.Area;
using AutoMapper;
using Domain.Interfaces;

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
        #endregion

        public async Task<DetailedReadAreaDTO> Execute(Guid? id, UpdateAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);
            entity.UpdateMainArea(dto.MainAreaId);

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadAreaDTO>(model);
        }
    }
}