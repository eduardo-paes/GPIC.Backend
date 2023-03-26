using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases.MainArea;
using AutoMapper;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.MainArea
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

        public async Task<DetailedMainAreaOutput> Execute(Guid? id, UpdateMainAreaInput dto)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Atualiza atributos permitidos
            entity.Name = dto.Name;
            entity.Code = dto.Code;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedMainAreaOutput>(model);
        }
    }
}