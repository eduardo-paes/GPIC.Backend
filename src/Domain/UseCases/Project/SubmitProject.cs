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
                UseCaseException.NotInformedParam(project.WorkType1 == null, nameof(project.WorkType1));
                UseCaseException.NotInformedParam(project.WorkType2 == null, nameof(project.WorkType2));
                UseCaseException.NotInformedParam(project.IndexedConferenceProceedings == null, nameof(project.IndexedConferenceProceedings));
                UseCaseException.NotInformedParam(project.NotIndexedConferenceProceedings == null, nameof(project.NotIndexedConferenceProceedings));
                UseCaseException.NotInformedParam(project.CompletedBook == null, nameof(project.CompletedBook));
                UseCaseException.NotInformedParam(project.OrganizedBook == null, nameof(project.OrganizedBook));
                UseCaseException.NotInformedParam(project.BookChapters == null, nameof(project.BookChapters));
                UseCaseException.NotInformedParam(project.BookTranslations == null, nameof(project.BookTranslations));
                UseCaseException.NotInformedParam(project.ParticipationEditorialCommittees == null, nameof(project.ParticipationEditorialCommittees));
                UseCaseException.NotInformedParam(project.FullComposerSoloOrchestraAllTracks == null, nameof(project.FullComposerSoloOrchestraAllTracks));
                UseCaseException.NotInformedParam(project.FullComposerSoloOrchestraCompilation == null, nameof(project.FullComposerSoloOrchestraCompilation));
                UseCaseException.NotInformedParam(project.ChamberOrchestraInterpretation == null, nameof(project.ChamberOrchestraInterpretation));
                UseCaseException.NotInformedParam(project.IndividualAndCollectiveArtPerformances == null, nameof(project.IndividualAndCollectiveArtPerformances));
                UseCaseException.NotInformedParam(project.ScientificCulturalArtisticCollectionsCuratorship == null, nameof(project.ScientificCulturalArtisticCollectionsCuratorship));
                UseCaseException.NotInformedParam(project.PatentLetter == null, nameof(project.PatentLetter));
                UseCaseException.NotInformedParam(project.PatentDeposit == null, nameof(project.PatentDeposit));
                UseCaseException.NotInformedParam(project.SoftwareRegistration == null, nameof(project.SoftwareRegistration));

                // Altera o status do projeto para submetido
                project.Status = EProjectStatus.Submitted;
                project.StatusDescription = EProjectStatus.Submitted.GetDescription();
                project.SubmitionDate = DateTime.Now;

                // Salva alterações no banco de dados
                await _projectRepository.Update(project);

                // Mapeia entidade para output e retorna
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }
            else
            {
                throw new ArgumentException("O projeto não se encontra num estágio que permita submissão.");
            }
        }
    }
}