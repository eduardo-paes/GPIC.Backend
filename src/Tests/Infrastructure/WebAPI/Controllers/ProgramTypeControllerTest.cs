using System.Net.Http.Json;
using Adapters.DTOs.ProgramType;
using Infrastructure.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Tests.Infrastructure.WebAPI.Controllers
{
    [TestFixture]
    public class ProgramTypeControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly string _baseAddress = "/api/programtype";

        public ProgramTypeControllerTests()
        {
            // Configura o servidor de teste
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            // Cria um cliente HTTP para testar a API
            _client = _server.CreateClient();
        }

        [Test, Order(2)]
        public async Task GetAllProgramTypes_ReturnsAllProgramTypes()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(_baseAddress);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var programtypes = await response.Content.ReadFromJsonAsync<IEnumerable<ResumedReadProgramTypeDTO>>();
            Assert.NotNull(programtypes);
            Assert.Greater(programtypes.Count(), 0);
        }

        [Test, Order(3)]
        public async Task GetProgramTypeById_ReturnsProgramType()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var programtypes = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadProgramTypeDTO>>();
            var expectedProgramType = programtypes.First();

            // Act
            var response = await _client.GetAsync($"{_baseAddress}/{expectedProgramType.Id}");
            var actualProgramType = await response.Content.ReadFromJsonAsync<ResumedReadProgramTypeDTO>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual(expectedProgramType.Id, actualProgramType.Id);
            Assert.AreEqual(expectedProgramType.Name, actualProgramType.Name);
        }

        [Test, Order(1)]
        public async Task CreateProgramType_ReturnsCreatedProgramType()
        {
            // Arrange
            var createProgramType = new CreateProgramTypeDTO { Name = $"Test ProgramType {DateTime.Now:ddMMyyyyhhmmss}", Description = "Test Description" };

            // Act
            var response = await _client.PostAsJsonAsync(_baseAddress, createProgramType);
            var createdProgramType = await response.Content.ReadFromJsonAsync<DetailedReadProgramTypeDTO>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(createdProgramType);
            Assert.AreNotEqual(createdProgramType?.Id, null);
            Assert.AreEqual(createProgramType.Name, createdProgramType?.Name);
        }

        [Test, Order(4)]
        public async Task UpdateProgramType_ReturnsUpdatedProgramType()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var programtypes = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadProgramTypeDTO>>();
            var updateProgramTypeDTO = new DetailedReadProgramTypeDTO { Id = programtypes.First().Id, Name = "Update Test", Description = "Update Test" };
            var id = programtypes.First().Id;

            // Act
            var response = await _client.PutAsJsonAsync($"{_baseAddress}/{updateProgramTypeDTO.Id}", updateProgramTypeDTO);
            var updatedProgramType = await response.Content.ReadFromJsonAsync<DetailedReadProgramTypeDTO>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(updatedProgramType);
            Assert.AreEqual(updateProgramTypeDTO.Id, updatedProgramType?.Id);
            Assert.AreEqual(updateProgramTypeDTO.Name, updatedProgramType?.Name);
        }

        [Test, Order(5)]
        public async Task DeleteProgramType_ReturnDeletedContent()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var programtypes = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadProgramTypeDTO>>();
            var id = programtypes.First().Id;

            // Act
            var response = await _client.DeleteAsync($"{_baseAddress}/{id}");
            var deletedProgramType = await response.Content.ReadFromJsonAsync<DetailedReadProgramTypeDTO>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(deletedProgramType);
            Assert.AreEqual(deletedProgramType.Id, id);
        }

        public void Dispose()
        {
            // Descarta o servidor de teste e o cliente HTTP após a conclusão dos testes
            _client.Dispose();
            _server.Dispose();
        }
    }
}
