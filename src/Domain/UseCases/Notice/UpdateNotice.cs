using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;
using Domain.Contracts.Activity;

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

            // Verifica se atividades foram informadas
            UseCaseException.BusinessRuleViolation(input.Activities == null || input.Activities.Count == 0,
                "As atividades devem ser informadas.");

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Notice));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(notice.DeletedAt != null, "O Edital inserido já foi excluído.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                entity.DocUrl = await _storageFileService.UploadFileAsync(input.File, entity.DocUrl);

            // Atualiza atributos permitidos
            entity.RegistrationStartDate = input.RegistrationStartDate ?? entity.RegistrationStartDate;
            entity.RegistrationEndDate = input.RegistrationEndDate ?? entity.RegistrationEndDate;
            entity.EvaluationStartDate = input.EvaluationStartDate ?? entity.EvaluationStartDate;
            entity.EvaluationEndDate = input.EvaluationEndDate ?? entity.EvaluationEndDate;
            entity.AppealStartDate = input.AppealStartDate ?? entity.AppealStartDate;
            entity.AppealEndDate = input.AppealEndDate ?? entity.AppealEndDate;
            entity.SendingDocsStartDate = input.SendingDocsStartDate ?? entity.SendingDocsStartDate;
            entity.SendingDocsEndDate = input.SendingDocsEndDate ?? entity.SendingDocsEndDate;
            entity.PartialReportDeadline = input.PartialReportDeadline ?? entity.PartialReportDeadline;
            entity.FinalReportDeadline = input.FinalReportDeadline ?? entity.FinalReportDeadline;
            entity.SuspensionYears = input.SuspensionYears ?? entity.SuspensionYears;

            // Salva entidade atualizada no banco
            await _repository.Update(notice);

            // Recupera atividades do edital
            var noticeActivities = await _activityTypeRepository.GetByNoticeId(notice.Id);

            // Atualiza atividades
            await HandleActivityType(input.Activities!, noticeActivities, notice.Id);

            // Retorna entidade atualizada
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }

        /// <summary>
        /// Atualiza tipos de atividades e atividades.
        /// </summary>
        /// <param name="newActivityTypes">Lista de tipos de atividades que serão atualizados.</param>
        /// <param name="oldActivityTypes">Lista de tipos de atividades que serão excluídos.</param>
        /// <param name="noticeId">Id do edital.</param>
        async Task HandleActivityType(IList<UpdateActivityTypeInput> newActivityTypes, IList<Entities.ActivityType> oldActivityTypes, Guid? noticeId)
        {
            foreach (var newActivityType in newActivityTypes)
            {
                // Verifica se o tipo de atividade já existe
                var activityType = oldActivityTypes.FirstOrDefault(x => x.Id == newActivityType.Id);

                // Se o tipo de atividade não existir, cria um novo
                if (activityType is null)
                {
                    // Cria tipo de atividade
                    activityType = new Entities.ActivityType(newActivityType.Name, newActivityType.Unity, noticeId);

                    // Salva tipo de atividade no banco
                    await _activityTypeRepository.Create(activityType);

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
                    await _activityTypeRepository.Update(activityType);

                    // Atualiza atividades
                    await HandleActivity(newActivityType.Activities!, activityType.Activities!, activityType.Id);

                    // Remove tipo de atividade da lista de tipos de atividades do edital
                    oldActivityTypes.Remove(activityType);
                }
            }

            // TODO: Validar remoção de tipos de atividade.
            // Verifica se existem tipos de atividades que foram excluídos
            foreach (var activityTypeToRemove in oldActivityTypes)
            {
                // Remove tipo de atividade do banco
                await _activityTypeRepository.Delete(activityTypeToRemove.Id);
            }
        }

        /// <summary>
        /// Atualiza atividades.
        /// </summary>
        /// <param name="newActivities">Lista de atividades que serão atualizadas.</param>
        /// <param name="oldActivities">Lista de atividades que serão excluídas.</param>
        /// <param name="activityTypeId">Id do tipo de atividade.</param>
        async Task HandleActivity(IList<UpdateActivityInput> newActivities, IList<Entities.Activity> oldActivities, Guid? activityTypeId)
        {
            // Verifica se existem atividades que foram criadas ou atualizadas
            foreach (var newActivity in newActivities)
            {
                // Verifica se o tipo de atividade já existe
                var activity = oldActivities.FirstOrDefault(x => x.Id == newActivity.Id);

                // Se o tipo de atividade não existir, cria um novo
                if (activity is null)
                {
                    // Cria atividade
                    activity = new Entities.Activity(newActivity.Name, newActivity.Points, newActivity.Limits, activityTypeId);

                    // Salva atividade no banco
                    await _activityRepository.Create(activity);
                }

                // Se o tipo de atividade existir, atualiza
                else
                {
                    // Atualiza atividade
                    activity.Name = newActivity.Name;
                    activity.Points = newActivity.Points;
                    activity.Limits = newActivity.Limits;

                    // Salva atividade atualizada no banco
                    await _activityRepository.Update(activity);

                    // Remove atividade da lista de atividades do tipo de atividade
                    oldActivities.Remove(activity);
                }
            }

            // TODO: Validar remoção de atividades.
            // Verifica se existem atividades que foram excluídas
            foreach (var activityToRemove in oldActivities)
            {
                // Remove atividade do banco
                await _activityRepository.Delete(activityToRemove.Id);
            }
        }
    }
}