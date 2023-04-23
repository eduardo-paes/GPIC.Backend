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
    Task<string> UploadNoticeFileAsync(IFormFile file, string? filePath = null);

    /// <summary>
    /// Realiza o upload de um arquivo de documento do aluno
    /// </summary>
    /// <param name="file">Documento do aluno em pdf ou imagem</param>
    /// <param name="filePath">Caminho completo até o arquivo</param>
    /// <returns>Caminho final do arquivo</returns>
    Task<string> UploadStudentDocFileAsync(IFormFile file, string? filePath = null);

    /// <summary>
    /// Realiza o upload de um arquivo de relatório (parcial ou final)
    /// </summary>
    /// <param name="file">Relatório em pdf</param>
    /// <param name="filePath">Caminho completo até o arquivo</param>
    /// <returns>Caminho final do arquivo</returns>
    Task<string> UploadReportFileAsync(IFormFile file, string? filePath = null);

    /// <summary>
    /// Deleta um arquivo
    /// </summary>
    /// <param name="filePath">Caminho completo até o arquivo</param>
    void DeleteFile(string filePath);
}