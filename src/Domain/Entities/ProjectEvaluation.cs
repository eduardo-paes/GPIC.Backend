using Domain.Entities.Enums;
using Domain.Entities.Primitives;

namespace Domain.Entities
{
    public class ProjectEvaluation : Entity
    {
        #region Properties
        #region Informações Gerais da Avaliação
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// O professor é bolsista de Produtividade?
        /// </summary>
        public bool IsProductivityFellow { get; set; }

        /// <summary>
        /// Id do avaliador que avaliou a submissão.
        /// </summary>
        public Guid? SubmissionEvaluatorId { get; set; }

        /// <summary>
        /// Data da avaliação da submissão.
        /// </summary>
        public DateTime? SubmissionEvaluationDate { get; set; }

        /// <summary>
        /// Nota da avaliação da submissão.
        /// </summary>
        public string? SubmissionEvaluationDescription { get; set; }

        /// <summary>
        /// Id do avaliador que avaliou o recurso.
        /// </summary>
        public Guid? AppealEvaluatorId { get; set; }

        /// <summary>
        /// Data da avaliação do recurso.
        /// </summary>
        public DateTime? AppealEvaluationDate { get; set; }

        /// <summary>
        /// Nota da avaliação do recurso.
        /// </summary>
        public string? AppealEvaluationDescription { get; set; }

        /// <summary>
        /// Id do avaliador que avaliou a documentação do projeto.
        /// </summary>
        public Guid? DocumentsEvaluatorId { get; set; }

        /// <summary>
        /// Data da avaliação da documentação do projeto.
        /// </summary>
        public DateTime? DocumentsEvaluationDate { get; set; }

        /// <summary>
        /// Nota da avaliação da documentação do projeto.
        /// </summary>
        public string? DocumentsEvaluationDescription { get; set; }

        public virtual Project? Project { get; }
        public virtual User? SubmissionEvaluator { get; }
        public virtual User? AppealEvaluator { get; }
        public virtual User? DocumentsEvaluator { get; }
        #endregion Informações Gerais da Avaliação

        #region (Resultados) Produção Científica - Trabalhos Publicados
        public int? FoundWorkType1 { get; set; }
        public int? FoundWorkType2 { get; set; }
        public int? FoundIndexedConferenceProceedings { get; set; }
        public int? FoundNotIndexedConferenceProceedings { get; set; }
        public int? FoundCompletedBook { get; set; }
        public int? FoundOrganizedBook { get; set; }
        public int? FoundBookChapters { get; set; }
        public int? FoundBookTranslations { get; set; }
        public int? FoundParticipationEditorialCommittees { get; set; }
        #endregion

        #region (Resultados) Produção Artístca e Cultural - Produção Apresentada
        public int? FoundFullComposerSoloOrchestraAllTracks { get; set; }
        public int? FoundFullComposerSoloOrchestraCompilation { get; set; }
        public int? FoundChamberOrchestraInterpretation { get; set; }
        public int? FoundIndividualAndCollectiveArtPerformances { get; set; }
        public int? FoundScientificCulturalArtisticCollectionsCuratorship { get; set; }
        #endregion

        #region (Resultados) Produção Técnica - Produtos Registrados
        public int? FoundPatentLetter { get; set; }
        public int? FoundPatentDeposit { get; set; }
        public int? FoundSoftwareRegistration { get; set; }
        #endregion

        #region Critérios de Avaliação
        /// <summary>
        /// Pontuação Total (Índice AP).
        /// </summary>
        public int? APIndex { get; set; }

        /// <summary>
        /// Titulação do Orientador.
        /// Doutor (2); Mestre (1).
        /// </summary>
        public EQualification? Qualification { get; set; }

        /// <summary>
        /// Foco e clareza quanto aos objetivos da proposta de projeto a ser desenvolvido pelo aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? ProjectProposalObjectives { get; set; }

        /// <summary>
        /// Coerência entre a produção acadêmico-científica do orientador e a proposta de projeto.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? AcademicScientificProductionCoherence { get; set; }

        /// <summary>
        /// Adequação da metodologia da proposta aos objetivos e ao cronograma de execução.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? ProposalMethodologyAdaptation { get; set; }

        /// <summary>
        /// Contribuição efetiva da proposta de projeto para formação em pesquisa do aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? EffectiveContributionToResearch { get; set; }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public ProjectEvaluation() { }
        #endregion
    }
}