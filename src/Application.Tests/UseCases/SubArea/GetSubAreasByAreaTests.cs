using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.UseCases.SubArea;
using Application.Ports.SubArea;
using Application.UseCases.SubArea;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.SubArea
{
    public class GetSubAreasByAreaTests
    {
        private readonly Mock<ISubAreaRepository> _subAreaRepositoryMock = new Mock<ISubAreaRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IGetSubAreasByArea CreateUseCase() => new GetSubAreasByArea(_subAreaRepositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.SubArea MockValidSubArea() => new(Guid.NewGuid(), "ABC", "SubArea Name");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadSubAreaOutputs()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid areaId = Guid.NewGuid();
            int skip = 0;
            int take = 5;
            var subAreas = new List<Domain.Entities.SubArea> { MockValidSubArea(), MockValidSubArea() };

            _subAreaRepositoryMock.Setup(repo => repo.GetSubAreasByAreaAsync(areaId, skip, take))
                .ReturnsAsync(subAreas);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(It.IsAny<IEnumerable<Domain.Entities.SubArea>>()))
                .Returns(subAreas.Select(sa => new ResumedReadSubAreaOutput()));

            // Act
            var result = await useCase.ExecuteAsync(areaId, skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // Check the expected count
            _subAreaRepositoryMock.Verify(repo => repo.GetSubAreasByAreaAsync(areaId, skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(It.IsAny<IEnumerable<Domain.Entities.SubArea>>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_InvalidSkipOrTake_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid areaId = Guid.NewGuid();
            int skip = -1; // Invalid skip value
            int take = 5;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(areaId, skip, take));
            _subAreaRepositoryMock.Verify(repo => repo.GetSubAreasByAreaAsync(areaId, skip, take), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(It.IsAny<IEnumerable<Domain.Entities.SubArea>>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NullAreaId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid areaId = Guid.NewGuid();
            int skip = 0;
            int take = 5;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(null, skip, take));
            _subAreaRepositoryMock.Verify(repo => repo.GetSubAreasByAreaAsync(areaId, skip, take), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(It.IsAny<IEnumerable<Domain.Entities.SubArea>>()), Times.Never);
        }
    }
}
