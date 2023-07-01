using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;

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
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Notice));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(entity.DeletedAt != null, "The notice entered has already been deleted.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                entity.DocUrl = await _storageFileService.UploadFileAsync(input.File, entity.DocUrl);

            // Atualiza atributos permitidos
            entity.StartDate = input.StartDate ?? entity.StartDate;
            entity.FinalDate = input.FinalDate ?? entity.FinalDate;
            entity.AppealStartDate = input.AppealStartDate ?? entity.AppealStartDate;
            entity.AppealFinalDate = input.AppealFinalDate ?? entity.AppealFinalDate;
            entity.SuspensionYears = input.SuspensionYears ?? entity.SuspensionYears;
            entity.SendingDocumentationDeadline = input.SendingDocumentationDeadline ?? entity.SendingDocumentationDeadline;
            entity.Description = input.Description ?? entity.Description;

            // Salva entidade atualizada no banco
            await _repository.Update(entity);

            // Retorna entidade atualizada
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}