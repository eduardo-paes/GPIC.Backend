using System.Net.Http.Json;
using Adapters.DTOs.Student;
using Infrastructure.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Tests.Infrastructure.WebAPI.Mocks;

namespace Tests.Infrastructure.WebAPI.Controllers
{
    [TestFixture]
    public class StudentControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly string _baseAddress = "/api/Student";
        private CreateUserMock _createUserMock;

        public StudentControllerTests()
        {
            // Configura o servidor de teste
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            // Cria um cliente HTTP para testar a API
            _client = _server.CreateClient();

            _createUserMock = new CreateUserMock();
        }

        [Test, Order(2)]
        public async Task GetAllStudent_ReturnsAllStudent()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(_baseAddress);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var students = await response.Content.ReadFromJsonAsync<IEnumerable<ResumedReadStudentDTO>>();
            Assert.NotNull(students);
            Assert.Greater(students.Count(), 0);
        }

        // [Test, Order(3)]
        // public async Task GetStudentById_ReturnsStudent()
        // {
        //     // Arrange
        //     var getAll = await _client.GetAsync(_baseAddress);
        //     var students = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadStudentDTO>>();
        //     var expectedStudent = students.First();

        //     // Act
        //     var response = await _client.GetAsync($"{_baseAddress}/{expectedStudent.Id}");
        //     var actualStudent = await response.Content.ReadFromJsonAsync<ResumedReadStudentDTO>();

        //     // Assert
        //     response.EnsureSuccessStatusCode(); // Status Code 200-299
        //     Assert.AreEqual(expectedStudent.Id, actualStudent.Id);
        //     Assert.AreEqual(expectedStudent.Name, actualStudent.Name);
        // }

        [Test, Order(1)]
        public async Task CreateStudent_ReturnsCreatedStudent()
        {
            // Arrange
            var createStudent = _createUserMock;

            // Act
            var response = await _client.PostAsJsonAsync(_baseAddress, createStudent);
            var createdStudent = await response.Content.ReadFromJsonAsync<DetailedReadStudentDTO>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(createdStudent);
            Assert.AreNotEqual(createdStudent?.Id, null);
            Assert.AreEqual(createStudent.RG, createdStudent?.RG);
        }

        // [Test, Order(4)]
        // public async Task UpdateStudent_ReturnsUpdatedStudent()
        // {
        //     // Arrange
        //     var getAll = await _client.GetAsync(_baseAddress);
        //     var students = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadStudentDTO>>();
        //     var updateStudentDTO = new DetailedReadStudentDTO { Id = students.First().Id, Name = "Update Test" };

        //     // Act
        //     var response = await _client.PutAsJsonAsync($"{_baseAddress}/{updateStudentDTO.Id}", updateStudentDTO);
        //     var updatedStudent = await response.Content.ReadFromJsonAsync<DetailedReadStudentDTO>();

        //     // Assert
        //     response.EnsureSuccessStatusCode(); // Status Code 200-299
        //     Assert.NotNull(updatedStudent);
        //     Assert.AreEqual(updateStudentDTO.Id, updatedStudent?.Id);
        //     Assert.AreEqual(updateStudentDTO.Name, updatedStudent?.Name);
        // }

        // [Test, Order(5)]
        // public async Task DeleteStudent_ReturnDeletedContent()
        // {
        //     // Arrange
        //     var getAll = await _client.GetAsync(_baseAddress);
        //     var students = await getAll.Content.ReadFromJsonAsync<IEnumerable<ResumedReadStudentDTO>>();
        //     var id = students.First().Id;

        //     // Act
        //     var response = await _client.DeleteAsync($"{_baseAddress}/{id}");
        //     var deletedStudent = await response.Content.ReadFromJsonAsync<DetailedReadStudentDTO>();

        //     // Assert
        //     response.EnsureSuccessStatusCode(); // Status Code 200-299
        //     Assert.NotNull(deletedStudent);
        //     Assert.AreEqual(deletedStudent.Id, id);
        // }

        public void Dispose()
        {
            // Descarta o servidor de teste e o cliente HTTP após a conclusão dos testes
            _client.Dispose();
            _server.Dispose();
        }
    }
}
