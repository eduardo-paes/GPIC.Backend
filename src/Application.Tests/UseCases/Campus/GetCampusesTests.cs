using Application.Ports.Campus;
using Application.UseCases.Campus;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Campus
{
    public class GetCampusesTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsQueryableOfResumedReadCampusOutput()
        {
            // Arrange
            var repositoryMock = new Mock<ICampusRepository>();
            var mapperMock = new Mock<IMapper>();
            var useCase = new GetCampuses(repositoryMock.Object, mapperMock.Object);
            int skip = 0;
            int take = 10;
            var campuses = new List<Domain.Entities.Campus>
            {
                new("Campus 1"),
                new("Campus 2"),
                new("Campus 3")
            };
            repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(campuses);
            var resumedReadCampusOutputs = campuses.Select(campus => new ResumedReadCampusOutput { Name = campus.Name }).ToList();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadCampusOutput>>(campuses)).Returns(resumedReadCampusOutputs);

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(campuses.Count, result.Count());
            repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadCampusOutput>>(campuses), Times.Once);
        }
    }
}
