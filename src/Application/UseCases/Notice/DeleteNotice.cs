using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Application.Validation;

namespace Application.UseCases.Notice
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

        public async Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Remove a entidade
            var entity = await _repository.DeleteAsync(id);

            // Deleta o arquivo do edital
            if (!string.IsNullOrEmpty(entity.DocUrl))
            {
                await _storageFileService.DeleteFileAsync(entity.DocUrl);
            }

            // Retorna o edital removido
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}