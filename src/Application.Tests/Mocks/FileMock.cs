using Microsoft.AspNetCore.Http;
using Moq;

namespace Application.Tests.Mocks
{
    public static class FileMock
    {
        public static IFormFile CreateIFormFile()
        {
            // Create a mock IFormFile, you can adjust the implementation as needed.
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(file => file.FileName).Returns("file.txt");
            fileMock.Setup(file => file.Length).Returns(1024);
            // Add other setup as needed.
            return fileMock.Object;
        }
    }
}