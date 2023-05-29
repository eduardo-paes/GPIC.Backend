using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw UseCaseException.NotInformedParam(nameof(id));

            // Remove a entidade
            var entity = await _repository.Delete(id);

            // Deleta o arquivo do edital
            if (!string.IsNullOrEmpty(entity.DocUrl))
                await _storageFileService.DeleteFile(entity.DocUrl);

            // Retorna o edital removido
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}