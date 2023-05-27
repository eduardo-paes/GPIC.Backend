using System.Net.Http.Json;
using Adapters.Gateways.Course;
using Infrastructure.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Tests.Infrastructure.WebAPI.Controllers
{
    [TestFixture]
    public class CourseControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly string _baseAddress = "/api/course";

        public CourseControllerTests()
        {
            // Configura o servidor de teste
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            // Cria um cliente HTTP para testar a API
            _client = _server.CreateClient();
        }

        [Test, Order(2)]
        public async Task GetAllCourses_ReturnsAllCourses()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(_baseAddress);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courses = await response.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCourseResponse>>();
            Assert.NotNull(courses);
            Assert.Greater(courses.Count(), 0);
        }

        [Test, Order(3)]
        public async Task GetCourseById_ReturnsCourse()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var courses = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCourseResponse>>();
            var expectedCourse = courses.First();

            // Act
            var response = await _client.GetAsync($"{_baseAddress}/{expectedCourse.Id}");
            var actualCourse = await response.Content.ReadFromJsonAsync<ResumedReadCourseResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual(expectedCourse.Id, actualCourse.Id);
            Assert.AreEqual(expectedCourse.Name, actualCourse.Name);
        }

        [Test, Order(1)]
        public async Task CreateCourse_ReturnsCreatedCourse()
        {
            // Arrange
            var createCourse = new CreateCourseRequest { Name = $"Test Course {DateTime.Now:ddMMyyyyhhmmss}" };

            // Act
            var response = await _client.PostAsJsonAsync(_baseAddress, createCourse);
            var createdCourse = await response.Content.ReadFromJsonAsync<DetailedReadCourseResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(createdCourse);
            Assert.AreNotEqual(createdCourse?.Id, null);
            Assert.AreEqual(createCourse.Name, createdCourse?.Name);
        }

        [Test, Order(4)]
        public async Task UpdateCourse_ReturnsUpdatedCourse()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var courses = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCourseResponse>>();
            var updateCourse = new DetailedReadCourseResponse { Id = courses.First().Id, Name = "Update Test" };
            var id = courses.First().Id;

            // Act
            var response = await _client.PutAsJsonAsync($"{_baseAddress}/{updateCourse.Id}", updateCourse);
            var updatedCourse = await response.Content.ReadFromJsonAsync<DetailedReadCourseResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(updatedCourse);
            Assert.AreEqual(updateCourse.Id, updatedCourse?.Id);
            Assert.AreEqual(updateCourse.Name, updatedCourse?.Name);
        }

        [Test, Order(5)]
        public async Task DeleteCourse_ReturnDeletedContent()
        {
            // Arrange
            var getAll = await _client.GetAsync(_baseAddress);
            var courses = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadCourseResponse>>();
            var id = courses.First().Id;

            // Act
            var response = await _client.DeleteAsync($"{_baseAddress}/{id}");
            var deletedCourse = await response.Content.ReadFromJsonAsync<DetailedReadCourseResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(deletedCourse);
            Assert.AreEqual(deletedCourse.Id, id);
        }

        public void Dispose()
        {
            // Descarta o servidor de teste e o cliente HTTP após a conclusão dos testes
            _client.Dispose();
            _server.Dispose();
        }
    }
}
