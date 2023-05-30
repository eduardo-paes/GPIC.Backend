using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Tests.Domain.Entities
{
    public class StudentAssistanceScholarshipTests
    {
        private StudentAssistanceScholarship MockValidStudentAssistanceScholarship() =>
            new StudentAssistanceScholarship(Guid.NewGuid(), "Scholarship Name", "Scholarship Description");

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();
            var name = "Scholarship Name";

            // Act
            scholarship.Name = name;

            // Assert
            scholarship.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullOrEmptyName_ThrowsException()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Name = null);
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Name = string.Empty);
        }

        [Fact]
        public void SetName_TooShortName_ThrowsException()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Name = "AB");
        }

        [Fact]
        public void SetName_TooLongName_ThrowsException()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Name = new string('A', 1500));
        }

        [Fact]
        public void SetDescription_ValidDescription_SetsDescription()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();
            var description = "Scholarship Description";

            // Act
            scholarship.Description = description;

            // Assert
            scholarship.Description.Should().Be(description);
        }

        [Fact]
        public void SetDescription_NullOrEmptyDescription_ThrowsException()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Description = null);
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Description = string.Empty);
        }

        [Fact]
        public void SetDescription_TooShortDescription_ThrowsException()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Description = "AB");
        }

        [Fact]
        public void SetDescription_TooLongDescription_ThrowsException()
        {
            // Arrange
            var scholarship = MockValidStudentAssistanceScholarship();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => scholarship.Description = "K5SYFiBmSfsadfsaBtfdsgfsdQkwCPPXKaTqoVtz7WF0LpFMG6hCu5sk8dBEbrhNDrBrd8WVt3vNp8uzhkck3nrYPVCMHvnDRnmdqlWRScH8yUYzoeLsiTvnROfHFm0GCsIltVcGiOBE6rkHCgjFCFJiSAdnBJOj54Godr0lRXRWBb8iIthDiPIvVKmtfRzH61ojFjezbENdzGgJisDzjg8zEogLia5D5cJKhGPGAgXsl1lDlFy2H8RvMZGwNg9UFyvRnRo6MYkxGLw6TemiwLBR99z5A3tEWUi9VVRhZpqWDvDYpEP4YaLfyTVcmPiHOtLxTMvkmSmWH3Z6O7uH69KJYLu2BqrNTe60bdu1Gvsk12N4LLT1aMmm2o679oqgxUAjws3PCchl2oHnDoX8qe5LywOuSanuknRUpRNnmcLNgbIxlrlPbFPxgokg3jt80dFi8KmyFQFV1p9sBBlr4V1i64ToundA5R7SLh7lNwpR9DuyzT90FgQs8ZYXlkspolRSLFV8ohYfHomvX2GanuJ4LXgkBnOtERsHQYhD9yqDphAgaeVMkfeUhsNyn3PGW0alcQ1WHaS3HllN2g0aMz6UUjn7JzEXwfUl7WO0muT4MfFKUh1mED5wgANs5nt65DVc1AWLkLXd0RNUILwRIDydtr4QrkeMJUpXx3DFX2w5eTjmY69dNCct8gEnOGZ2F4YhveYVPgNcAABMBKri8AutNxPoPvEDk36sYqMEFsHpM9i5gHQtwtRe8HiGmvneeEGUAK7wsyJG7FBHXjU2Rvcxd2uLq24AEJOO6TABzePdNeqrh2y9h08p0ZTIGih9gT7rOXcURfJ2zn6pKY0CHOrQhaSK9xgBOgExdCRFI4mtMoIrlAJL8OLYDlckR1YalMv2ypWzHPUkVkXob3MrbfD0F5MTNZocl7VAwf0xAGsKgae4JrfK8iS3E3ZyfyYw29Qu4vUMRY6VxK1GY02z1wJpNiI0LjkiOHlL3rYxD9YeOkJLOCq382bMdy5A0NJkqSiWrqkK3z733iOSKp1PQ8zIUCkhTvX1Zig3OPRISAKaHt4hqkU8NpEtwXyjGYM7LaUNfecztIUnAkIswrsJYcErFDEQvnIbcAMCTZgwynhU06FxBKyDQc4rY2AeEnFOkE9pvleIO3VEDWgfNgAv5oCzn8U2JKTyuAHRXf0xoGOAsw23NVwaVROVpkP4uYUyVLLePOiSmiGiHz2fjH4TdLtvRjQvmT40eXB7dAxbOVjZcIwwP4Pi6Kq04Eb70kJbpckEGPPYfLewJdhiAnmiIgDBVn7tgIY9RmknnaDANuZiZNiRCO9Il63KtrlW8o3RZdI1lW3mSIQkFOVIx7JICtB6WDBTonZbYZ6zqcB1ut1efAkoTKnEsO2jWz2QOpoLMP7NVThFcEjt7lruRy3SPsZxwnAmsaQywXztsHxHod5KrRqxhuuVjt2nWGutyal7vw0qjIV8ugDATYS4Nq2hOBnK5t94HeTavgTrN8nx24");
        }
    }
}
