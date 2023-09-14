using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.UseCases.AssistanceType;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.AssistanceType
{
    public class GetAssistanceTypesTests
    {
        private readonly Mock<IAssistanceTypeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetAssistanceTypes CreateUseCase()
        {
            return new GetAssistanceTypes(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsQueryableOfResumedReadAssistanceTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;
            var assistanceTypes = new List<Domain.Entities.AssistanceType>
            {
                new(Guid.NewGuid(), "AssistanceType1", "Description"),
                new(Guid.NewGuid(), "AssistanceType2", "Description"),
                new(Guid.NewGuid(), "AssistanceType3", "Description")
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(assistanceTypes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadAssistanceTypeOutput>>(assistanceTypes)).Returns(assistanceTypes.Select(at => new ResumedReadAssistanceTypeOutput { Id = at.Id, Name = at.Name }));

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IQueryable<ResumedReadAssistanceTypeOutput>>(result);
            Assert.Equal(3, result.Count());
            _repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadAssistanceTypeOutput>>(assistanceTypes), Times.Once);
        }
    }
}
