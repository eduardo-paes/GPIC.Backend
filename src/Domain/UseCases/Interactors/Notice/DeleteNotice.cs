using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.Notice;
using Domain.UseCases.Ports.Notice;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Notice
{
    public class DeleteNotice : IDeleteNotice
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;
        public DeleteNotice(INoticeRepository repository, IStorageFileService storageFileService, IMapper mapper)
        {
            _repository = repository;
            _storageFileService = storageFileService;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Remove a entidade
            Entities.Notice entity = await _repository.Delete(id);

            // Deleta o arquivo do edital
            if (!string.IsNullOrEmpty(entity.DocUrl))
            {
                await _storageFileService.DeleteFile(entity.DocUrl);
            }

            // Retorna o edital removido
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}