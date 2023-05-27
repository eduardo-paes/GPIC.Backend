using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases
{
    public class CreateNotice : ICreateNotice
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IStorageFileService _storageFileService;
        private readonly IMapper _mapper;
        public CreateNotice(INoticeRepository repository, IStorageFileService storageFileService, IMapper mapper)
        {
            _repository = repository;
            _storageFileService = storageFileService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(CreateNoticeInput input)
        {
            // Verifica se as datas foram informadas
            if (input.StartDate == null)
                throw new ArgumentNullException(nameof(input.StartDate));
            if (input.FinalDate == null)
                throw new ArgumentNullException(nameof(input.FinalDate));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetNoticeByPeriod((DateTime)input.StartDate, (DateTime)input.FinalDate);
            if (entity != null)
                throw new Exception($"Já existe um Edital para o período indicado.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                input.DocUrl = await _storageFileService.UploadFileAsync(input.File);

            // Cria entidade
            entity = _mapper.Map<Entities.Notice>(input);
            entity = await _repository.Create(entity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}