using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class StorageFileService : IStorageFileService
    {
        #region Global Scope
        private readonly string _directory;
        private readonly string?[] _allowedExtensions;
        private readonly long _maxFileSizeInBytes;
        private readonly string _noticeDirectory;
        private readonly string _studentDocDirectory;
        private readonly string _reportDirectory;

        public StorageFileService(IConfiguration configuration)
        {
            configuration = InitializeConfigs();

            // Verifica se o diretório de armazenamento de arquivos foi configurado
            _directory = configuration["StorageFile:Directory"]
                ?? throw new Exception("O diretório de armazenamento de arquivos não foi configurado.");

            // Verifica se as extensões de arquivos permitidas foram configuradas
            var allowedExtensions = configuration.GetSection("StorageFile:AllowedExtensions");
            if (allowedExtensions == null)
                throw new Exception("As extensões de arquivos permitidas não foram configuradas.");
            _allowedExtensions = allowedExtensions.GetChildren().Select(x => x.Value).ToArray();

            // Verifica se o tamanho máximo de arquivo foi configurado
            if (long.TryParse(configuration["StorageFile:MaxFileSizeInBytes"], out long maxFileSizeInBytes))
                _maxFileSizeInBytes = maxFileSizeInBytes;
            else
                throw new Exception("O tamanho máximo de arquivo não foi configurado.");

            // Verifica se o diretório de armazenamento de arquivos dos editais foi configurado
            _noticeDirectory = configuration["StorageFile:NoticeDirectory"]
                ?? throw new Exception("O diretório de armazenamento de arquivos não foi configurado.");

            // Verifica se o diretório de armazenamento de arquivos dos documentos dos alunos foi configurado
            _studentDocDirectory = configuration["StorageFile:StudentDocDirectory"]
                ?? throw new Exception("O diretório de armazenamento de arquivos não foi configurado.");

            // Verifica se o diretório de armazenamento de arquivos dos relatórios foi configurado
            _reportDirectory = configuration["StorageFile:ReportDirectory"]
                ?? throw new Exception("O diretório de armazenamento de arquivos não foi configurado.");
        }
        #endregion

        #region Public Methods
        public async Task<string> UploadNoticeFileAsync(IFormFile file, string? filePath = null)
        {
            // Valida o arquivo
            filePath = GenerateFilePath(file, _noticeDirectory, filePath, true);

            // Salva o arquivo
            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            // Retorna o caminho do arquivo
            return filePath;
        }

        public async Task<string> UploadStudentDocFileAsync(IFormFile file, string? filePath = null)
        {
            // Valida o arquivo
            filePath = GenerateFilePath(file, _studentDocDirectory, filePath);

            // Salva o arquivo
            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            // Retorna o caminho do arquivo
            return filePath;
        }

        public async Task<string> UploadReportFileAsync(IFormFile file, string? filePath = null)
        {
            // Valida o arquivo
            filePath = GenerateFilePath(file, _reportDirectory, filePath, true);

            // Salva o arquivo
            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            // Retorna o caminho do arquivo
            return filePath;
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"O arquivo {filePath} não pode ser excluído.", ex);
            }
        }
        #endregion

        #region Private Methods
        private string GenerateFilePath(IFormFile file, string custom_directory, string? filePath = null, bool onlyPdf = false)
        {
            // Verifica se a extensão do arquivo é permitida
            var extension = Path.GetExtension(file.FileName);
            if ((onlyPdf && extension != ".pdf") || (!_allowedExtensions.Contains(extension)))
                throw new Exception($"A extensão {extension} do arquivo não é permitida.");

            // Verifica o tamanho do arquivo
            if (file.Length > _maxFileSizeInBytes)
                throw new Exception($"O tamanho do arquivo excede o máximo de {_maxFileSizeInBytes} bytes.");

            // Gera um nome único para o arquivo
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Gera o caminho do arquivo se não for informado
            if (string.IsNullOrEmpty(filePath))
            {
                // Cria o diretório caso não exista
                string dirPath = Path.Combine(_directory, custom_directory);
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                // Gera o caminho do arquivo
                filePath = Path.Combine(dirPath, fileName);
            }
            // Deleta o arquivo se o caminho do arquivo for informado
            else
            {
                DeleteFile(filePath);
            }
            return filePath;
        }

        private static IConfiguration InitializeConfigs()
        {
            // Adicione o caminho base para o arquivo appsettings.json
            var basePath = Path.GetDirectoryName(typeof(StorageFileService).Assembly.Location);
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath!)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // Use a configuração criada acima para ler as configurações do appsettings.json
            return configurationBuilder.Build();
        }
        #endregion
    }
}