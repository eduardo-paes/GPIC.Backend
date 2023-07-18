using Domain.Contracts.StudentDocuments;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases
{
    public class UpdateStudentDocuments : IUpdateStudentDocuments
    {
        #region Global Scope
        private readonly IStudentDocumentsRepository _studentDocumentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;

        public UpdateStudentDocuments(
            IStudentDocumentsRepository studentDocumentsRepository,
            IProjectRepository projectRepository,
            IStorageFileService storageFileService,
            IMapper mapper)
        {
            _studentDocumentRepository = studentDocumentsRepository;
            _projectRepository = projectRepository;
            _storageFileService = storageFileService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadStudentDocumentsOutput> Execute(Guid? id, UpdateStudentDocumentsInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se já foram enviados documentos para o projeto informado
            var studentDocuments = await _studentDocumentRepository.GetById(id!);
            UseCaseException.NotFoundEntityById(studentDocuments is not null, nameof(studentDocuments));

            // Verifica se o projeto se encontra em situação de submissão de documentos
            UseCaseException.BusinessRuleViolation(
                studentDocuments!.Project?.Status != Entities.Enums.EProjectStatus.DocumentAnalysis
                    || studentDocuments!.Project?.Status != Entities.Enums.EProjectStatus.Pending,
                "O projeto não está na fase de apresentação de documentos.");

            // Atualiza entidade a partir do input informado
            studentDocuments!.AgencyNumber = input.AgencyNumber;
            studentDocuments!.AccountNumber = input.AccountNumber;

            // Atualiza arquivos na nuvem
            await TryToSaveFileInCloud(input.IdentityDocument!, studentDocuments.IdentityDocument);
            await TryToSaveFileInCloud(input.CPF!, studentDocuments.CPF);
            await TryToSaveFileInCloud(input.Photo3x4!, studentDocuments.Photo3x4);
            await TryToSaveFileInCloud(input.SchoolHistory!, studentDocuments.SchoolHistory);
            await TryToSaveFileInCloud(input.ScholarCommitmentAgreement!, studentDocuments.ScholarCommitmentAgreement);
            await TryToSaveFileInCloud(input.ParentalAuthorization!, studentDocuments.ParentalAuthorization);
            await TryToSaveFileInCloud(input.AccountOpeningProof!, studentDocuments.AccountOpeningProof);

            // Atualiza entidade
            studentDocuments = await _studentDocumentRepository.Update(studentDocuments);

            // Se o projeto está no status de pendente, atualiza para o status de análise de documentos
            if (studentDocuments.Project?.Status == Entities.Enums.EProjectStatus.Pending)
            {
                var project = await _projectRepository.GetById(studentDocuments.ProjectId);
                project!.Status = Entities.Enums.EProjectStatus.DocumentAnalysis;
                await _projectRepository.Update(project);
            }

            // Retorna entidade atualizada
            return _mapper.Map<DetailedReadStudentDocumentsOutput>(studentDocuments);
        }

        private async Task TryToSaveFileInCloud(IFormFile file, string? url)
        {
            try
            {
                if (file is null) return;
                await _storageFileService.UploadFileAsync(file, url);
            }
            catch (Exception ex)
            {
                throw UseCaseException.BusinessRuleViolation($"Erro ao salvar arquivos na nuvem.\n{ex}");
            }
        }
    }
}