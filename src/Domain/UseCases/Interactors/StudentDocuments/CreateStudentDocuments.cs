using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.StudentDocuments;
using Domain.UseCases.Ports.StudentDocuments;
using Domain.Validation;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases
{
    public class CreateStudentDocuments : ICreateStudentDocuments
    {
        #region Global Scope
        private readonly IStudentDocumentsRepository _studentDocumentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Lista de arquivos que foram salvos na nuvem para remoção em caso de erro.
        /// </summary>
        private readonly List<string> _urlFiles;

        public CreateStudentDocuments(
            IStudentDocumentsRepository studentDocumentsRepository,
            IProjectRepository projectRepository,
            IStorageFileService storageFileService,
            IMapper mapper)
        {
            _studentDocumentRepository = studentDocumentsRepository;
            _projectRepository = projectRepository;
            _storageFileService = storageFileService;
            _mapper = mapper;

            _urlFiles = new List<string>();
        }
        #endregion

        public async Task<DetailedReadStudentDocumentsOutput> Execute(CreateStudentDocumentsInput input)
        {
            // Verifica se já há documentos para o projeto informado
            var documents = await _studentDocumentRepository.GetByProjectId(input.ProjectId!);
            UseCaseException.BusinessRuleViolation(documents is null, "Já existem documentos do aluno para o projeto indicado.");

            // Verifica se o projeto existe
            var project = await _projectRepository.GetById(input.ProjectId!);
            UseCaseException.NotFoundEntityById(project is null, nameof(Entities.Project));

            // Verifica se o projeto se encontra em situação de submissão de documentos (Aceito)
            UseCaseException.BusinessRuleViolation(
                project?.Status != Entities.Enums.EProjectStatus.Accepted,
                "O projeto não está na fase de apresentação de documentos.");

            // Cria entidade a partir do input informado
            var entity = new Entities.StudentDocuments(input.ProjectId, input.AgencyNumber, input.AccountNumber);

            // Verifica se o aluno é menor de idade
            if (project?.Student?.BirthDate > DateTime.UtcNow.AddYears(-18))
            {
                // Verifica se foi informado a autorização dos pais
                UseCaseException.BusinessRuleViolation(input.ParentalAuthorization is null,
                    "A autorização dos pais deve ser fornecida para alunos menores de idade.");

                // Salva autorização dos pais
                entity.ParentalAuthorization = await TryToSaveFileInCloud(input.ParentalAuthorization!);
            }

            // Salva demais arquivos na nuvem
            entity.IdentityDocument = await TryToSaveFileInCloud(input.IdentityDocument!);
            entity.CPF = await TryToSaveFileInCloud(input.CPF!);
            entity.Photo3x4 = await TryToSaveFileInCloud(input.Photo3x4!);
            entity.SchoolHistory = await TryToSaveFileInCloud(input.SchoolHistory!);
            entity.ScholarCommitmentAgreement = await TryToSaveFileInCloud(input.ScholarCommitmentAgreement!);
            entity.AccountOpeningProof = await TryToSaveFileInCloud(input.AccountOpeningProof!);

            // Cria entidade
            entity = await _studentDocumentRepository.Create(entity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentDocumentsOutput>(entity);
        }

        private async Task<string> TryToSaveFileInCloud(IFormFile file)
        {
            try
            {
                string url = await _storageFileService.UploadFileAsync(file);
                _urlFiles.Add(url);
                return url;
            }
            catch (Exception ex)
            {
                // Caso dê erro, remove da nuvem os arquivos que foram salvos
                foreach (var url in _urlFiles)
                    await _storageFileService.DeleteFile(url);
                throw UseCaseException.BusinessRuleViolation($"Erro ao salvar arquivos na nuvem.\n{ex}");
            }
        }
    }
}