using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Application.Validation;

namespace Application.UseCases.Notice
{
    public class CreateNotice : ICreateNotice
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IStorageFileService _storageFileService;
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public CreateNotice(
            INoticeRepository repository,
            IStorageFileService storageFileService,
            IActivityTypeRepository activityTypeRepository,
            IActivityRepository activityRepository,
            IProfessorRepository professorRepository,
            IEmailService emailService,
            IMapper mapper)
        {
            _repository = repository;
            _storageFileService = storageFileService;
            _activityTypeRepository = activityTypeRepository;
            _activityRepository = activityRepository;
            _professorRepository = professorRepository;
            _emailService = emailService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadNoticeOutput> ExecuteAsync(CreateNoticeInput input)
        {
            // Verifica se atividades foram informadas
            UseCaseException.BusinessRuleViolation(input.Activities == null || input.Activities.Count == 0,
                "As atividades devem ser informadas.");

            // Mapeia input para entidade
            var notice = new Domain.Entities.Notice(
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
            var noticeFound = await _repository.GetNoticeByPeriodAsync((DateTime)input.RegistrationStartDate!, (DateTime)input.RegistrationEndDate!);
            UseCaseException.BusinessRuleViolation(noticeFound != null, "Já existe um Edital para o período indicado.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                notice.DocUrl = await _storageFileService.UploadFileAsync(input.File);

            // Converte as atividades para entidades antes de prosseguir 
            // com o cadastro no banco, apenas para fins de validação.
            foreach (var activityType in input.Activities!)
            {
                // Converte atividades para entidades
                foreach (var activity in activityType.Activities!)
                    _ = new Domain.Entities.Activity(activity.Name, activity.Points, activity.Limits, Guid.Empty);

                // Converte tipo de atividade para entidade
                _ = new Domain.Entities.ActivityType(activityType.Name, activityType.Unity, Guid.Empty);
            }

            // Cria edital no banco
            notice = await _repository.CreateAsync(notice);

            // Salva atividades no banco
            foreach (var activityType in input.Activities!)
            {
                // Salva tipo de atividade no banco
                var activityTypeEntity = new Domain.Entities.ActivityType(activityType.Name, activityType.Unity, notice.Id);
                activityTypeEntity = await _activityTypeRepository.CreateAsync(activityTypeEntity);

                // Salva atividades no banco
                foreach (var activity in activityType.Activities!)
                {
                    var activityEntity = new Domain.Entities.Activity(activity.Name, activity.Points, activity.Limits, activityTypeEntity.Id);
                    await _activityRepository.CreateAsync(activityEntity);
                }
            }

            // Obtém professores ativos
            var professors = await _professorRepository.GetAllActiveProfessorsAsync();

            // Envia email de notificação para todos os professores ativos
            foreach (var professor in professors)
            {
                // Envia email de notificação
                await _emailService.SendNoticeEmailAsync(
                    professor.User!.Email,
                    professor.User!.Name,
                    notice.RegistrationStartDate,
                    notice.RegistrationEndDate,
                    notice.DocUrl);
            }

            // Salva entidade no banco
            return _mapper.Map<DetailedReadNoticeOutput>(notice);
        }
    }
}