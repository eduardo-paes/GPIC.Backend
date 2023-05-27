using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases
{
    public class UpdateNotice : IUpdateNotice
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;
        public UpdateNotice(INoticeRepository repository, IStorageFileService storageFileService, IMapper mapper)
        {
            _repository = repository;
            _storageFileService = storageFileService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput input)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se as datas foram informadas
            if (input.StartDate == null)
                throw new ArgumentNullException(nameof(input.StartDate));
            if (input.FinalDate == null)
                throw new ArgumentNullException(nameof(input.FinalDate));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se entidade existe
            if (entity == null)
                throw new Exception("Edital não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Edital informado já foi excluído.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                entity.DocUrl = await _storageFileService.UploadFileAsync(input.File, entity.DocUrl);

            // Atualiza atributos permitidos
            entity.StartDate = input.StartDate;
            entity.FinalDate = input.FinalDate;
            entity.Description = input.Description;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadNoticeOutput>(model);
        }
    }
}