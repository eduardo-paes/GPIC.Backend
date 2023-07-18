using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Project;
using Domain.Validation;

namespace Domain.UseCases.Project
{
    public class SubmitProject : ISubmitProject
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public SubmitProject(IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResumedReadProjectOutput> Execute(Guid? projectId)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Verifica se o projeto existe
            var project = await _projectRepository.GetById(projectId!.Value)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o projeto está aberto
            if (project.Status == EProjectStatus.Opened)
            {
                // Verifica se todos os campos do projeto foram preenchidos
                project.WorkType1 ??= 0;
                project.WorkType2 ??= 0;
                project.IndexedConferenceProceedings ??= 0;
                project.NotIndexedConferenceProceedings ??= 0;
                project.CompletedBook ??= 0;
                project.OrganizedBook ??= 0;
                project.BookChapters ??= 0;
                project.BookTranslations ??= 0;
                project.ParticipationEditorialCommittees ??= 0;
                project.FullComposerSoloOrchestraAllTracks ??= 0;
                project.FullComposerSoloOrchestraCompilation ??= 0;
                project.ChamberOrchestraInterpretation ??= 0;
                project.IndividualAndCollectiveArtPerformances ??= 0;
                project.ScientificCulturalArtisticCollectionsCuratorship ??= 0;
                project.PatentLetter ??= 0;
                project.PatentDeposit ??= 0;
                project.SoftwareRegistration ??= 0;

                // Altera o status do projeto para submetido
                project.Status = EProjectStatus.Submitted;
                project.StatusDescription = EProjectStatus.Submitted.GetDescription();
                project.SubmissionDate = DateTime.UtcNow;

                // Salva alterações no banco de dados
                await _projectRepository.Update(project);

                // Mapeia entidade para output e retorna
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }
            else
            {
                throw UseCaseException.BusinessRuleViolation("The project is not at a stage that allows submission.");
            }
        }
    }
}