using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases.Notice;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Notice
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
                throw new ArgumentNullException(nameof(id));

            // Verifica se o edital existe
            var model = await _repository.Delete(id);
            if (model == null)
                throw new Exception("Edital não encontrado.");

            // Deleta o arquivo do edital
            if (!string.IsNullOrEmpty(model.DocUrl))
                _storageFileService.DeleteFile(model.DocUrl);

            // Retorna o edital removido
            return _mapper.Map<DetailedReadNoticeOutput>(model);
        }
    }
}