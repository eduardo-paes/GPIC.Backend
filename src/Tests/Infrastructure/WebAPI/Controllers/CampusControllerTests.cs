using System.Net.Http.Json;
using Adapters.Gateways.Campus;
using Infrastructure.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Tests.Infrastructure.WebAPI.Controllers
{
    [TestFixture]
    public class CampusControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly string _baseAddress = "/api/campus";

        public CampusControllerTests()
        {
            // Configura o servidor de teste
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            // Cria um cliente HTTP para testar a API
            _client = _server.CreateClient();
        }

        [Test, Order(2)]
        public async Task GetAllCampus_ReturnsAllCampus()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(_baseAddress);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var campuses = await response.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCampusResponse>>();
            Assert.NotNull(campuses);
            Assert.Greater(campuses.Count(), 0);
        }

        [Test, Order(3)]
        public async Task GetCampusById_ReturnsCampus()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var campuses = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCampusResponse>>();
            var expectedCampus = campuses.First();

            // Act
            var response = await _client.GetAsync($"{_baseAddress}/{expectedCampus.Id}");
            var actualCampus = await response.Content.ReadFromJsonAsync<ResumedReadCampusResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual(expectedCampus.Id, actualCampus.Id);
            Assert.AreEqual(expectedCampus.Name, actualCampus.Name);
        }

        [Test, Order(1)]
        public async Task CreateCampus_ReturnsCreatedCampus()
        {
            // Arrange
            var createCampus = new CreateCampusRequest { Name = $"Test Campus {DateTime.Now:ddMMyyyyhhmmss}" };

            // Act
            var response = await _client.PostAsJsonAsync(_baseAddress, createCampus);
            var createdCampus = await response.Content.ReadFromJsonAsync<DetailedReadCampusResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(createdCampus);
            Assert.AreNotEqual(createdCampus?.Id, null);
            Assert.AreEqual(createCampus.Name, createdCampus?.Name);
        }

        [Test, Order(4)]
        public async Task UpdateCampus_ReturnsUpdatedCampus()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var campuses = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCampusResponse>>();
            var updateCampus = new DetailedReadCampusResponse { Id = campuses.First().Id, Name = "Update Test" };

            // Act
            var response = await _client.PutAsJsonAsync($"{_baseAddress}/{updateCampus.Id}", updateCampus);
            var updatedCampus = await response.Content.ReadFromJsonAsync<DetailedReadCampusResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(updatedCampus);
            Assert.AreEqual(updateCampus.Id, updatedCampus?.Id);
            Assert.AreEqual(updateCampus.Name, updatedCampus?.Name);
        }

        [Test, Order(5)]
        public async Task DeleteCampus_ReturnDeletedContent()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var campuses = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCampusResponse>>();
            var id = campuses.First().Id;

            // Act
            var response = await _client.DeleteAsync($"{_baseAddress}/{id}");
            var deletedCampus = await response.Content.ReadFromJsonAsync<DetailedReadCampusResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(deletedCampus);
            Assert.AreEqual(deletedCampus.Id, id);
        }

        public void Dispose()
        {
            // Descarta o servidor de teste e o cliente HTTP após a conclusão dos testes
            _client.Dispose();
            _server.Dispose();
        }
    }
}
