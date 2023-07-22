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
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        public UpdateNotice(
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

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Recupera entidade que será atualizada
            var notice = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Notice));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(notice.DeletedAt != null, "The notice entered has already been deleted.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                notice.DocUrl = await _storageFileService.UploadFileAsync(input.File, notice.DocUrl);

            // Atualiza atributos permitidos
            notice.RegistrationStartDate = input.RegistrationStartDate ?? notice.RegistrationStartDate;
            notice.RegistrationEndDate = input.RegistrationEndDate ?? notice.RegistrationEndDate;
            notice.EvaluationStartDate = input.EvaluationStartDate ?? notice.EvaluationStartDate;
            notice.EvaluationEndDate = input.EvaluationEndDate ?? notice.EvaluationEndDate;
            notice.AppealStartDate = input.AppealStartDate ?? notice.AppealStartDate;
            notice.AppealEndDate = input.AppealEndDate ?? notice.AppealEndDate;
            notice.SendingDocsStartDate = input.SendingDocsStartDate ?? notice.SendingDocsStartDate;
            notice.SendingDocsEndDate = input.SendingDocsEndDate ?? notice.SendingDocsEndDate;
            notice.PartialReportDeadline = input.PartialReportDeadline ?? notice.PartialReportDeadline;
            notice.FinalReportDeadline = input.FinalReportDeadline ?? notice.FinalReportDeadline;
            notice.SuspensionYears = input.SuspensionYears ?? notice.SuspensionYears;

            // Converte as atividades para entidades antes de prosseguir 
            // com a atualização no banco, apenas para fins de validação.
            foreach (var activityType in input.Activities!)
            {
                // Converte atividades para entidades
                foreach (var activity in activityType.Activities!)
                    _ = new Entities.Activity(activity.Name, activity.Points, activity.Limits, Guid.Empty);

                // Converte tipo de atividade para entidade
                _ = new Entities.ActivityType(activityType.Name, activityType.Unity, Guid.Empty);
            }

            // Salva entidade atualizada no banco
            await _repository.Update(notice);

            // Recupera atividades do edital
            var noticeActivities = (await _activityTypeRepository.GetByNoticeId(notice.Id)).ToList();

            // Atualiza atividades
            foreach (var activityType in input.Activities)
            {
                // Verifica se o tipo de atividade é novo
                if (activityType.Id is null)
                {
                    // Cria tipo de atividade
                    var activityTypeEntity = new Entities.ActivityType(activityType.Name, activityType.Unity, notice.Id);

                    // Salva tipo de atividade no banco
                    await _activityTypeRepository.Create(activityTypeEntity);

                    // Cria atividades
                    foreach (var activity in activityType.Activities!)
                    {
                        // Cria atividade
                        var activityEntity = new Entities.Activity(activity.Name, activity.Points, activity.Limits, activityTypeEntity.Id);

                        // Salva atividade no banco
                        await _activityRepository.Create(activityEntity);
                    }
                }
                else
                {
                    // Verifica se o tipo de atividade foi excluído
                    var activityTypeEntity = noticeActivities.Find(x => x.Id == activityType.Id)
                        ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ActivityType));

                    // Atualiza tipo de atividade
                    activityTypeEntity.Name = activityType.Name;
                    activityTypeEntity.Unity = activityType.Unity;

                    // Atualiza atividades
                    foreach (var activity in activityType.Activities!)
                    {
                        // Verifica se a atividade foi excluída
                        var activityEntity = activityTypeEntity.Activities!.FirstOrDefault(x => x.Id == activity.Id)
                            ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Activity));

                        // Atualiza atividade
                        activityEntity.Name = activity.Name;
                        activityEntity.Points = activity.Points;
                        activityEntity.Limits = activity.Limits;

                        // Salva atividade atualizada no banco
                        await _activityRepository.Update(activityEntity);

                        // Remove atividade da lista de atividades do tipo de atividade
                        activityTypeEntity.Activities!.Remove(activityEntity);
                    }

                    // Salva tipo de atividade atualizado no banco
                    await _activityTypeRepository.Update(activityTypeEntity);

                    // Remove tipo de atividade da lista de tipos de atividades do edital
                    noticeActivities.Remove(activityTypeEntity);
                }
            }

            // Verifica se existem tipos de atividades que foram excluídos
            foreach (var activityTypeToRemove in noticeActivities)
            {
                foreach (var activityToRemove in activityTypeToRemove.Activities!)
                {
                    // Verifica se a atividade foi excluída
                    var activityEntity = activityTypeToRemove.Activities!.FirstOrDefault(x => x.Id == activityToRemove.Id)
                        ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Activity));

                    // Remove atividade do banco
                    await _activityRepository.Delete(activityEntity.Id);
                }

                // Remove tipo de atividade do banco
                await _activityTypeRepository.Delete(activityTypeToRemove.Id);
            }

            // Retorna entidade atualizada
            return _mapper.Map<DetailedReadNoticeOutput>(notice);
        }
    }
}