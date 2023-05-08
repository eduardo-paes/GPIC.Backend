using Azure.Storage.Blobs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class AzureStorageService : IStorageFileService
    {
        #region Global Scope
        private readonly string _container;
        private readonly string _connectionString;
        private readonly string?[] _allowedExtensions;
        private readonly long _maxFileSizeInBytes;

        public AzureStorageService(IConfiguration configuration, IDotEnvSecrets dotEnvSecrets)
        {
            // Verifica se o container de armazenamento de arquivos foi configurado
            _container = dotEnvSecrets.GetBlobStorageContainerName()
                ?? throw new Exception("O container de armazenamento de arquivos não foi configurado.");

            // Verifica se a string de conexão foi configurada
            _connectionString = dotEnvSecrets.GetBlobStorageConnectionString()
                ?? throw new Exception("A string de conexão não foi configurada.");

            // Verifica se as extensões de arquivos permitidas foram configuradas
            var allowedExtensions = configuration.GetSection("StorageFile:AllowedExtensions")
                ?? throw new Exception("As extensões de arquivos permitidas não foram configuradas.");
            _allowedExtensions = allowedExtensions.GetChildren().Select(x => x.Value).ToArray();

            // Verifica se o tamanho máximo de arquivo foi configurado
            if (long.TryParse(configuration["StorageFile:MaxFileSizeInBytes"], out long maxFileSizeInBytes))
                _maxFileSizeInBytes = maxFileSizeInBytes;
            else
                throw new Exception("O tamanho máximo de arquivo não foi configurado.");
        }
        #endregion

        public async Task DeleteFile(string url)
        {
            // Cria o cliente do blob
            var fileName = url.Split("/").LastOrDefault();

            // Cria o cliente do blob
            var blobClient = new BlobClient(_connectionString, _container, fileName);

            // Deleta o arquivo
            await blobClient.DeleteAsync();
        }

        public async Task<string> UploadFileAsync(IFormFile file, string? fileName = null)
        {
            // Remove o arquivo anterior caso já exista
            if (!string.IsNullOrEmpty(fileName))
            {
                // Deleta o arquivo
                await DeleteFile(fileName);

                // Utiliza o mesmo nome do arquivo anterior para o arquivo atual
                fileName = fileName.Split("/").LastOrDefault();
            }
            // Gera um nome único para o arquivo
            else
            {
                fileName = GenerateFileName(file);
            }

            // Cria o cliente do blob
            var blobClient = new BlobClient(_connectionString, _container, fileName);

            // Converte o arquivo para um array de bytes
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            // Salva o arquivo
            using (var stream = new MemoryStream(fileBytes))
                await blobClient.UploadAsync(stream);

            // Retorna o caminho do arquivo
            return blobClient.Uri.AbsoluteUri;
        }

        #region Private Methods
        private string GenerateFileName(IFormFile file, bool onlyPdf = false)
        {
            // Verifica se a extensão do arquivo é permitida
            var extension = Path.GetExtension(file.FileName);
            if ((onlyPdf && extension != ".pdf") || (!_allowedExtensions.Contains(extension)))
                throw new Exception($"A extensão ({extension}) do arquivo não é permitida.");

            // Verifica o tamanho do arquivo
            if (file.Length > _maxFileSizeInBytes)
                throw new Exception($"O tamanho do arquivo excede o máximo de {_maxFileSizeInBytes} bytes.");

            // Gera um nome único para o arquivo
            return $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        }
        #endregion
    }
}