using Azure.Storage.Blobs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Services
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
            IConfigurationSection allowedExtensions = configuration.GetSection("StorageFile:AllowedExtensions")
                ?? throw new Exception("As extensões de arquivos permitidas não foram configuradas.");
            _allowedExtensions = allowedExtensions.GetChildren().Select(x => x.Value).ToArray();

            // Verifica se o tamanho máximo de arquivo foi configurado
            _maxFileSizeInBytes = long.TryParse(configuration["StorageFile:MaxFileSizeInBytes"], out long maxFileSizeInBytes)
                ? maxFileSizeInBytes
                : throw new Exception("O tamanho máximo de arquivo não foi configurado.");
        }
        #endregion Global Scope

        public async Task DeleteFileAsync(string filePath)
        {
            // Cria o cliente do blob
            string? fileName = filePath.Split("/").LastOrDefault();

            // Cria o cliente do blob
            BlobClient blobClient = new(_connectionString, _container, fileName);

            // Deleta o arquivo
            _ = await blobClient.DeleteAsync();
        }

        public async Task<string> UploadFileAsync(IFormFile file, string? filePath = null)
        {
            // Remove o arquivo anterior caso já exista
            if (!string.IsNullOrEmpty(filePath))
            {
                // Deleta o arquivo
                await DeleteFileAsync(filePath);

                // Utiliza o mesmo nome do arquivo anterior para o arquivo atual
                filePath = filePath.Split("/").LastOrDefault();
            }
            // Gera um nome único para o arquivo
            else
            {
                filePath = GenerateFileName(file);
            }

            // Cria o cliente do blob
            BlobClient blobClient = new(_connectionString, _container, filePath);

            // Converte o arquivo para um array de bytes
            byte[] fileBytes;
            using (MemoryStream ms = new())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            // Salva o arquivo
            using (MemoryStream stream = new(fileBytes))
            {
                _ = await blobClient.UploadAsync(stream);
            }

            // Retorna o caminho do arquivo
            return blobClient.Uri.AbsoluteUri;
        }

        #region Private Methods
        private string GenerateFileName(IFormFile file, bool onlyPdf = false)
        {
            // Verifica se a extensão do arquivo é permitida
            string extension = Path.GetExtension(file.FileName);
            if ((onlyPdf && extension != ".pdf") || !_allowedExtensions.Contains(extension))
            {
                throw new Exception($"A extensão ({extension}) do arquivo não é permitida.");
            }

            // Verifica o tamanho do arquivo
            if (file.Length > _maxFileSizeInBytes)
            {
                throw new Exception($"O tamanho do arquivo excede o máximo de {_maxFileSizeInBytes} bytes.");
            }

            // Gera um nome único para o arquivo
            return $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        }
        #endregion Private Methods
    }
}