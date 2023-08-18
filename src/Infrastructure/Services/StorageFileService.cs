using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class StorageFileService : IStorageFileService
    {
        #region Global Scope
        private readonly string _directory;
        private readonly string?[] _allowedExtensions;
        private readonly long _maxFileSizeInBytes;
        private readonly string _folder;

        public StorageFileService(IConfiguration configuration)
        {
            configuration = InitializeConfigs();

            // Verifica se o diretório de armazenamento de arquivos foi configurado
            _directory = configuration["StorageFile:Directory"]
                ?? throw new Exception("O diretório de armazenamento de arquivos não foi configurado.");

            // Verifica se as extensões de arquivos permitidas foram configuradas
            IConfigurationSection allowedExtensions = configuration.GetSection("StorageFile:AllowedExtensions")
                ?? throw new Exception("As extensões de arquivos permitidas não foram configuradas.");
            _allowedExtensions = allowedExtensions.GetChildren().Select(x => x.Value).ToArray();

            // Verifica se o tamanho máximo de arquivo foi configurado
            _maxFileSizeInBytes = long.TryParse(configuration["StorageFile:MaxFileSizeInBytes"], out long maxFileSizeInBytes)
                ? maxFileSizeInBytes
                : throw new Exception("O tamanho máximo de arquivo não foi configurado.");

            // Verifica se o diretório de armazenamento de arquivos dos editais foi configurado
            _folder = configuration["StorageFile:Folder"]
                ?? throw new Exception("O diretório de armazenamento de arquivos não foi configurado.");
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<string> UploadFileAsync(IFormFile file, string? filePath = null)
        {
            // Valida o arquivo
            filePath = await GenerateFilePath(file, _folder, filePath, true);

            // Salva o arquivo
            using (FileStream stream = new(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorna o caminho do arquivo
            return filePath;
        }

        public async Task DeleteFileAsync(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception($"O arquivo {filePath} não pode ser excluído.", ex);
            }
        }

        public Task<string> UploadFileAsync(byte[] file, string? filePath)
        {
            throw new NotImplementedException();
        }
        #endregion Public Methods

        #region Private Methods
        private async Task<string> GenerateFilePath(IFormFile file, string custom_directory, string? filePath = null, bool onlyPdf = false)
        {
            // Verifica se a extensão do arquivo é permitida
            string extension = Path.GetExtension(file.FileName);
            if ((onlyPdf && extension != ".pdf") || !_allowedExtensions.Contains(extension))
            {
                throw new Exception($"A extensão {extension} do arquivo não é permitida.");
            }

            // Verifica o tamanho do arquivo
            if (file.Length > _maxFileSizeInBytes)
            {
                throw new Exception($"O tamanho do arquivo excede o máximo de {_maxFileSizeInBytes} bytes.");
            }

            // Gera um nome único para o arquivo
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Gera o caminho do arquivo se não for informado
            if (string.IsNullOrEmpty(filePath))
            {
                // Cria o diretório caso não exista
                string dirPath = Path.Combine(_directory, custom_directory);
                if (!Directory.Exists(dirPath))
                {
                    _ = Directory.CreateDirectory(dirPath);
                }

                // Gera o caminho do arquivo
                filePath = Path.Combine(dirPath, fileName);
            }
            // Deleta o arquivo se o caminho do arquivo for informado
            else
            {
                await DeleteFileAsync(filePath);
            }
            return filePath;
        }

        private static IConfiguration InitializeConfigs()
        {
            // Adicione o caminho base para o arquivo appsettings.json
            string? basePath = Path.GetDirectoryName(typeof(StorageFileService).Assembly.Location);
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath!)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // Use a configuração criada acima para ler as configurações do appsettings.json
            return configurationBuilder.Build();
        }
        #endregion Private Methods
    }
}