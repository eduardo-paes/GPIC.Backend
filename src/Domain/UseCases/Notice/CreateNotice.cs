using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;

namespace Domain.UseCases
{
    public class CreateNotice : ICreateNotice
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;
        public CreateNotice(INoticeRepository repository, IStorageFileService storageFileService, IMapper mapper)
        {
            _repository = repository;
            _storageFileService = storageFileService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(CreateNoticeInput input)
        {
            // Verifica se atividades foram informadas
            UseCaseException.BusinessRuleViolation(input.Activities == null || input.Activities.Count == 0,
                "As atividades devem ser informadas.");

            // Mapeia input para entidade
            var entity = new Entities.Notice(
                input.RegistrationStartDate,
                input.RegistrationEndDate,
                input.EvaluationStartDate,
                input.EvaluationEndDate,
                input.AppealStartDate,
                input.AppealEndDate,
                input.SendingDocsStartDate,
                input.SendingDocsEndDate,
                input.PartialReportDeadline,
                input.FinalReportDeadline,
                input.SuspensionYears
            );

            // Verifica se já existe um edital para o período indicado
            var noticeFound = await _repository.GetNoticeByPeriod((DateTime)input.RegistrationStartDate!, (DateTime)input.RegistrationEndDate!);
            UseCaseException.BusinessRuleViolation(noticeFound != null, "Já existe um Edital para o período indicado.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                entity.DocUrl = await _storageFileService.UploadFileAsync(input.File);

            // Cria entidade
            entity = await _repository.Create(entity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}