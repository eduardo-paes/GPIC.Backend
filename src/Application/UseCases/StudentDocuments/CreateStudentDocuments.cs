using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.Validation;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.StudentDocuments
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

        public async Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(CreateStudentDocumentsInput input)
        {
            // Verifica se já há documentos para o projeto informado
            var documents = await _studentDocumentRepository.GetByProjectIdAsync(input.ProjectId!);
            UseCaseException.BusinessRuleViolation(documents is null,
                "Já existem documentos do aluno para o projeto indicado.");

            // Verifica se o projeto existe
            var project = await _projectRepository.GetByIdAsync(input.ProjectId!);
            UseCaseException.NotFoundEntityById(project is null, nameof(Domain.Entities.Project));

            // Verifica se o projeto se encontra em situação de submissão de documentos (Aceito)
            UseCaseException.BusinessRuleViolation(
                project?.Status != Domain.Entities.Enums.EProjectStatus.Accepted,
                "O projeto não está na fase de apresentação de documentos.");

            // Cria entidade a partir do input informado
            var studentDocument = new Domain.Entities.StudentDocuments(input.ProjectId, input.AgencyNumber, input.AccountNumber);

            // Verifica se o aluno é menor de idade
            if (project?.Student?.BirthDate > DateTime.UtcNow.AddYears(-18))
            {
                // Verifica se foi informado a autorização dos pais
                UseCaseException.BusinessRuleViolation(input.ParentalAuthorization is null,
                    "A autorização dos pais deve ser fornecida para alunos menores de idade.");

                // Salva autorização dos pais
                studentDocument.ParentalAuthorization = await TryToSaveFileInCloud(input.ParentalAuthorization!);
            }

            // Salva demais arquivos na nuvem
            studentDocument.IdentityDocument = await TryToSaveFileInCloud(input.IdentityDocument!);
            studentDocument.CPF = await TryToSaveFileInCloud(input.CPF!);
            studentDocument.Photo3x4 = await TryToSaveFileInCloud(input.Photo3x4!);
            studentDocument.SchoolHistory = await TryToSaveFileInCloud(input.SchoolHistory!);
            studentDocument.ScholarCommitmentAgreement = await TryToSaveFileInCloud(input.ScholarCommitmentAgreement!);
            studentDocument.AccountOpeningProof = await TryToSaveFileInCloud(input.AccountOpeningProof!);

            // Cria entidade
            studentDocument = await _studentDocumentRepository.CreateAsync(studentDocument);

            // Atualiza status do projeto
            project!.Status = Domain.Entities.Enums.EProjectStatus.DocumentAnalysis;

            // Salva alterações no banco de dados
            _ = await _projectRepository.UpdateAsync(project);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentDocumentsOutput>(studentDocument);
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
                    await _storageFileService.DeleteFileAsync(url);
                throw UseCaseException.BusinessRuleViolation($"Erro ao salvar arquivos na nuvem.\n{ex}");
            }
        }
    }
}