using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.Validation;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.StudentDocuments
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
        #endregion Global Scope

        public async Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(Guid? id, UpdateStudentDocumentsInput model)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se já foram enviados documentos para o projeto informado
            Domain.Entities.StudentDocuments? studentDocuments = await _studentDocumentRepository.GetByIdAsync(id!);
            UseCaseException.NotFoundEntityById(studentDocuments is not null, nameof(studentDocuments));

            // Verifica se o projeto se encontra em situação de submissão de documentos (Aceito ou Pendente do envio de documentação)
            UseCaseException.BusinessRuleViolation(
                studentDocuments!.Project?.Status is not Domain.Entities.Enums.EProjectStatus.Accepted
                    and not Domain.Entities.Enums.EProjectStatus.Pending,
                "O projeto não está na fase de apresentação de documentos.");

            // Atualiza entidade a partir do input informado
            studentDocuments!.AgencyNumber = model.AgencyNumber;
            studentDocuments!.AccountNumber = model.AccountNumber;

            // Atualiza arquivos na nuvem
            await TryToSaveFileInCloud(model.IdentityDocument!, studentDocuments.IdentityDocument);
            await TryToSaveFileInCloud(model.CPF!, studentDocuments.CPF);
            await TryToSaveFileInCloud(model.Photo3x4!, studentDocuments.Photo3x4);
            await TryToSaveFileInCloud(model.SchoolHistory!, studentDocuments.SchoolHistory);
            await TryToSaveFileInCloud(model.ScholarCommitmentAgreement!, studentDocuments.ScholarCommitmentAgreement);
            await TryToSaveFileInCloud(model.ParentalAuthorization!, studentDocuments.ParentalAuthorization);
            await TryToSaveFileInCloud(model.AccountOpeningProof!, studentDocuments.AccountOpeningProof);

            // Atualiza entidade
            studentDocuments = await _studentDocumentRepository.UpdateAsync(studentDocuments);

            // Se o projeto está no status de pendente, atualiza para o status de análise de documentos
            if (studentDocuments.Project?.Status == Domain.Entities.Enums.EProjectStatus.Pending)
            {
                Domain.Entities.Project? project = await _projectRepository.GetByIdAsync(studentDocuments.ProjectId);
                project!.Status = Domain.Entities.Enums.EProjectStatus.DocumentAnalysis;
                _ = await _projectRepository.UpdateAsync(project);
            }

            // Retorna entidade atualizada
            return _mapper.Map<DetailedReadStudentDocumentsOutput>(studentDocuments);
        }

        private async Task TryToSaveFileInCloud(IFormFile file, string? url)
        {
            try
            {
                if (file is not null)
                {
                    _ = await _storageFileService.UploadFileAsync(file, url);
                }
            }
            catch (Exception ex)
            {
                throw UseCaseException.BusinessRuleViolation($"Erro ao salvar arquivos na nuvem.\n{ex}");
            }
        }
    }
}