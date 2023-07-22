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
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        public CreateNotice(
            INoticeRepository repository,
            IStorageFileService storageFileService,
            IActivityTypeRepository activityTypeRepository,
            IActivityRepository activityRepository,
            IMapper mapper)
        {
            _repository = repository;
            _storageFileService = storageFileService;
            _activityTypeRepository = activityTypeRepository;
            _activityRepository = activityRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(CreateNoticeInput input)
        {
            // Verifica se atividades foram informadas
            UseCaseException.BusinessRuleViolation(input.Activities == null, "As atividades devem ser informadas.");

            // Mapeia input para entidade
            var notice = new Entities.Notice(
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
            var projectFound = await _repository.GetNoticeByPeriod((DateTime)input.RegistrationStartDate!, (DateTime)input.RegistrationEndDate!);
            UseCaseException.BusinessRuleViolation(projectFound != null, "Já existe um Edital para o período indicado.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                notice.DocUrl = await _storageFileService.UploadFileAsync(input.File);

            // Converte as atividades para entidades antes de prosseguir 
            // com o cadastro no banco, apenas para fins de validação.
            foreach (var activityType in input.Activities!)
            {
                // Converte atividades para entidades
                foreach (var activity in activityType.Activities!)
                    _ = new Entities.Activity(activity.Name, activity.Points, activity.Limits, Guid.Empty);

                // Converte tipo de atividade para entidade
                _ = new Entities.ActivityType(activityType.Name, activityType.Unity, Guid.Empty);
            }

            // Cria edital no banco
            notice = await _repository.Create(notice);

            // Salva atividades no banco
            foreach (var activityType in input.Activities!)
            {
                // Salva tipo de atividade no banco
                var activityTypeEntity = new Entities.ActivityType(activityType.Name, activityType.Unity, notice.Id);
                activityTypeEntity = await _activityTypeRepository.Create(activityTypeEntity);

                // Salva atividades no banco
                foreach (var activity in activityType.Activities!)
                {
                    var activityEntity = new Entities.Activity(activity.Name, activity.Points, activity.Limits, activityTypeEntity.Id);
                    await _activityRepository.Create(activityEntity);
                }
            }

            // Salva entidade no banco
            return _mapper.Map<DetailedReadNoticeOutput>(notice);
        }
    }
}