using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases.Notice;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Notice
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

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput dto)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se as datas foram informadas
            if (dto.StartDate == null)
                throw new ArgumentNullException(nameof(dto.StartDate));
            if (dto.FinalDate == null)
                throw new ArgumentNullException(nameof(dto.FinalDate));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Edital informado já foi excluído.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (dto.File != null)
                entity.DocUrl = await _storageFileService.UploadNoticeFileAsync(dto.File, entity.DocUrl);

            // Atualiza atributos permitidos
            entity.StartDate = dto.StartDate;
            entity.FinalDate = dto.FinalDate;
            entity.Description = dto.Description;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadNoticeOutput>(model);
        }
    }
}