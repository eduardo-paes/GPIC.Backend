using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services
{
    public interface IStorageFileService
    {
        Task<string> UploadNoticeFileAsync(IFormFile file);
    }
}