using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Ports.Activity;
using Domain.UseCases.Interfaces.Notice;
using Domain.UseCases.Ports.Activity;
using Domain.UseCases.Ports.Notice;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Notice
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
        #endregion Global Scope

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id, UpdateNoticeInput model)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Verifica se atividades foram informadas
            UseCaseException.BusinessRuleViolation(model.Activities == null || model.Activities.Count == 0,
                "As atividades devem ser informadas.");

            // Recupera entidade que será atualizada
            Entities.Notice notice = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Notice));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(notice.DeletedAt != null, "O Edital inserido já foi excluído.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (model.File != null)
            {
                notice.DocUrl = await _storageFileService.UploadFileAsync(model.File, notice.DocUrl);
            }

            // Atualiza atributos permitidos
            notice.RegistrationStartDate = model.RegistrationStartDate ?? notice.RegistrationStartDate;
            notice.RegistrationEndDate = model.RegistrationEndDate ?? notice.RegistrationEndDate;
            notice.EvaluationStartDate = model.EvaluationStartDate ?? notice.EvaluationStartDate;
            notice.EvaluationEndDate = model.EvaluationEndDate ?? notice.EvaluationEndDate;
            notice.AppealStartDate = model.AppealStartDate ?? notice.AppealStartDate;
            notice.AppealEndDate = model.AppealEndDate ?? notice.AppealEndDate;
            notice.SendingDocsStartDate = model.SendingDocsStartDate ?? notice.SendingDocsStartDate;
            notice.SendingDocsEndDate = model.SendingDocsEndDate ?? notice.SendingDocsEndDate;
            notice.PartialReportDeadline = model.PartialReportDeadline ?? notice.PartialReportDeadline;
            notice.FinalReportDeadline = model.FinalReportDeadline ?? notice.FinalReportDeadline;
            notice.SuspensionYears = model.SuspensionYears ?? notice.SuspensionYears;

            // Converte as atividades para entidades antes de prosseguir 
            // com a atualização no banco, apenas para fins de validação.
            foreach (UpdateActivityTypeInput activityType in model.Activities!)
            {
                // Converte atividades para entidades
                foreach (UpdateActivityInput activity in activityType.Activities!)
                {
                    _ = new Entities.Activity(activity.Name, activity.Points, activity.Limits, Guid.Empty);
                }

                // Converte tipo de atividade para entidade
                _ = new Entities.ActivityType(activityType.Name, activityType.Unity, Guid.Empty);
            }

            // Salva entidade atualizada no banco
            _ = await _repository.Update(notice);

            // Recupera atividades do edital
            IList<Entities.ActivityType> noticeActivities = await _activityTypeRepository.GetByNoticeId(notice.Id);

            // Atualiza atividades
            await HandleActivityType(model.Activities!, noticeActivities, notice.Id);

            // Retorna entidade atualizada
            return _mapper.Map<DetailedReadNoticeOutput>(notice);
        }

        /// <summary>
        /// Atualiza tipos de atividades e atividades.
        /// </summary>
        /// <param name="newActivityTypes">Lista de tipos de atividades que serão atualizados.</param>
        /// <param name="oldActivityTypes">Lista de tipos de atividades que serão excluídos.</param>
        /// <param name="noticeId">Id do edital.</param>
        private async Task HandleActivityType(IList<UpdateActivityTypeInput> newActivityTypes, IList<Entities.ActivityType> oldActivityTypes, Guid? noticeId)
        {
            foreach (UpdateActivityTypeInput newActivityType in newActivityTypes)
            {
                // Verifica se o tipo de atividade já existe
                Entities.ActivityType? activityType = oldActivityTypes.FirstOrDefault(x => x.Id == newActivityType.Id);

                // Se o tipo de atividade não existir, cria um novo
                if (activityType is null)
                {
                    // Cria tipo de atividade
                    activityType = new Entities.ActivityType(newActivityType.Name, newActivityType.Unity, noticeId);

                    // Salva tipo de atividade no banco
                    _ = await _activityTypeRepository.Create(activityType);

                    // Cria atividades
                    await HandleActivity(newActivityType.Activities!, new List<Entities.Activity>(), activityType.Id);
                }

                // Se o tipo de atividade existir, atualiza
                else
                {
                    // Atualiza tipo de atividade
                    activityType.Name = newActivityType.Name;
                    activityType.Unity = newActivityType.Unity;

                    // Salva tipo de atividade atualizado no banco
                    _ = await _activityTypeRepository.Update(activityType);

                    // Atualiza atividades
                    await HandleActivity(newActivityType.Activities!, activityType.Activities!, activityType.Id);

                    // Remove tipo de atividade da lista de tipos de atividades do edital
                    _ = oldActivityTypes.Remove(activityType);
                }
            }

            // TODO: Validar remoção de tipos de atividade.
            // Verifica se existem tipos de atividades que foram excluídos
            foreach (Entities.ActivityType activityTypeToRemove in oldActivityTypes)
            {
                // Remove tipo de atividade do banco
                _ = await _activityTypeRepository.Delete(activityTypeToRemove.Id);
            }
        }

        /// <summary>
        /// Atualiza atividades.
        /// </summary>
        /// <param name="newActivities">Lista de atividades que serão atualizadas.</param>
        /// <param name="oldActivities">Lista de atividades que serão excluídas.</param>
        /// <param name="activityTypeId">Id do tipo de atividade.</param>
        private async Task HandleActivity(IList<UpdateActivityInput> newActivities, IList<Entities.Activity> oldActivities, Guid? activityTypeId)
        {
            // Verifica se existem atividades que foram criadas ou atualizadas
            foreach (UpdateActivityInput newActivity in newActivities)
            {
                // Verifica se o tipo de atividade já existe
                Entities.Activity? activity = oldActivities.FirstOrDefault(x => x.Id == newActivity.Id);

                // Se o tipo de atividade não existir, cria um novo
                if (activity is null)
                {
                    // Cria atividade
                    activity = new Entities.Activity(newActivity.Name, newActivity.Points, newActivity.Limits, activityTypeId);

                    // Salva atividade no banco
                    _ = await _activityRepository.Create(activity);
                }

                // Se o tipo de atividade existir, atualiza
                else
                {
                    // Atualiza atividade
                    activity.Name = newActivity.Name;
                    activity.Points = newActivity.Points;
                    activity.Limits = newActivity.Limits;

                    // Salva atividade atualizada no banco
                    _ = await _activityRepository.Update(activity);

                    // Remove atividade da lista de atividades do tipo de atividade
                    _ = oldActivities.Remove(activity);
                }
            }

            // TODO: Validar remoção de atividades.
            // Verifica se existem atividades que foram excluídas
            foreach (Entities.Activity activityToRemove in oldActivities)
            {
                // Remove atividade do banco
                _ = await _activityRepository.Delete(activityToRemove.Id);
            }
        }
    }
}