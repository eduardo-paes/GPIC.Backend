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

        public async Task<DetailedReadNoticeOutput> Execute(CreateNoticeInput dto)
        {
            // Verifica se as datas foram informadas
            if (dto.StartDate == null)
                throw new ArgumentNullException(nameof(dto.StartDate));
            if (dto.FinalDate == null)
                throw new ArgumentNullException(nameof(dto.FinalDate));

            // Verifica se já existe um edital para o período indicado
            var entity = await _repository.GetNoticeByPeriod((DateTime)dto.StartDate, (DateTime)dto.FinalDate);
            if (entity != null)
                throw new Exception($"Já existe um Edital para o período indicado.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (dto.File != null)
                dto.DocUrl = await _storageFileService.UploadNoticeFileAsync(dto.File);

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.Notice>(dto));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}