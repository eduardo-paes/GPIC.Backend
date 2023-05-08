using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services;
public interface IStorageFileService
{
    /// <summary>
    /// Realiza o upload de um arquivo de edital
    /// </summary>
    /// <param name="file">Edital em pdf</param>
    /// <param name="filePath">Caminho completo até o arquivo</param>
    /// <returns>Caminho final do arquivo</returns>
    Task<string> UploadFileAsync(IFormFile file, string? filePath = null);

    /// <summary>
    /// Deleta um arquivo
    /// </summary>
    /// <param name="filePath">Caminho completo até o arquivo</param>
    Task DeleteFile(string filePath);
}