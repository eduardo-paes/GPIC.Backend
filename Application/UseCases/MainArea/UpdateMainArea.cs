using Application.DTOs.MainArea;
using Application.Proxies.MainArea;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.MainArea
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

        public async Task<ReadMainAreaDTO> Execute(Guid? id, UpdateMainAreaDTO dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Atualiza atributos permitidos
            entity.UpdateName(dto.Name);
            entity.UpdateCode(dto.Code);

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<ReadMainAreaDTO>(model);
        }
    }
}