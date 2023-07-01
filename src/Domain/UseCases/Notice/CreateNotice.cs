using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;

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
            // Mapeia input para entidade
            var entity = _mapper.Map<Entities.Notice>(input);

            // Verifica se já existe um edital para o período indicado
            var projectFound = await _repository.GetNoticeByPeriod((DateTime)input.StartDate!, (DateTime)input.FinalDate!);
            UseCaseException.BusinessRuleViolation(projectFound != null, "A notice already exists for the indicated period.");

            // Salva arquivo no repositório e atualiza atributo DocUrl
            if (input.File != null)
                input.DocUrl = await _storageFileService.UploadFileAsync(input.File);

            // Cria entidade
            entity = await _repository.Create(entity);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}