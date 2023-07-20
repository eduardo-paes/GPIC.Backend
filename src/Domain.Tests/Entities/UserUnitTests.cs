using System;
using System.Reflection;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using Xunit;

namespace Domain.Tests.Entities
{
    public class UserUnitTests
    {
        private User _user;
        private User MockValidUser()
        {
            if (_user == null)
                _user = InvokeInternalConstructor<User>("John Doe", "john.doe@example.com", "strongpassword", "92114660087", ERole.ADMIN);
            return _user;
        }

        [Fact]
        public void TestInternalConstructor()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "strongpassword";
            var cpf = "92114660087";
            var role = ERole.ADMIN;

            // Act
            var user = InvokeInternalConstructor<User>(name, email, password, cpf, role);

            // Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(cpf, user.CPF);
            Assert.Equal(role, user.Role);
        }

        private T InvokeInternalConstructor<T>(params object[] args)
        {
            var constructor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Any, args.Select(a => a?.GetType()).ToArray(), null);
            if (constructor == null)
                throw new InvalidOperationException("Internal constructor not found.");

            return (T)constructor.Invoke(args);
        }

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var user = MockValidUser();
            var name = "John Doe Updated";

            // Act
            user.Name = name;

            // Assert
            Assert.Equal(name, user.Name);
        }

        [Fact]
        public void SetName_NullOrEmptyName_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Name = null);
            Assert.Throws<EntityExceptionValidation>(() => user.Name = string.Empty);
        }

        [Fact]
        public void SetName_TooShortName_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Name = "AB");
        }

        [Fact]
        public void SetName_TooLongName_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Name = new string('A', 500));
        }

        [Fact]
        public void SetEmail_ValidEmail_SetsEmail()
        {
            // Arrange
            var user = MockValidUser();
            var email = "john.doe.updated@example.com";

            // Act
            user.Email = email;

            // Assert
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void SetEmail_NullOrEmptyEmail_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Email = null);
            Assert.Throws<EntityExceptionValidation>(() => user.Email = string.Empty);
        }

        [Fact]
        public void SetEmail_InvalidEmail_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Email = "invalid-email");
        }

        [Fact]
        public void SetEmail_TooLongEmail_ThrowsEntityExceptionValidation()
        {
            // Arrange
            var user = MockValidUser();
            var longEmail = new string('a', 300) + "@example.com";

            // Act & Assert
            var ex = Assert.Throws<EntityExceptionValidation>(() => user.Email = longEmail);
            // Assert.Equal("email", ex.PropertyName); // Ensure that the correct property triggered the exception
            Assert.Contains("email", ex.Message); // Ensure that the exception message contains the property name
        }

        [Fact]
        public void SetPassword_ValidPassword_SetsPassword()
        {
            // Arrange
            var user = MockValidUser();
            var password = "new-strong-password";

            // Act
            user.Password = password;

            // Assert
            Assert.Equal(password, user.Password);
        }

        [Fact]
        public void SetPassword_NullOrEmptyPassword_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Password = null);
            Assert.Throws<EntityExceptionValidation>(() => user.Password = string.Empty);
        }

        [Fact]
        public void SetPassword_TooShortPassword_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Password = "abc12");
        }

        [Fact]
        public void SetPassword_TooLongPassword_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();
            var longPassword = new string('a', 400);

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Password = longPassword);
        }

        [Fact]
        public void SetCPF_ValidCPF_SetsCPF()
        {
            // Arrange
            var user = MockValidUser();
            var cpf = "58247313065";

            // Act
            user.CPF = cpf;

            // Assert
            Assert.Equal(cpf, user.CPF);
        }

        [Fact]
        public void SetCPF_NullOrEmptyCPF_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.CPF = null);
            Assert.Throws<EntityExceptionValidation>(() => user.CPF = string.Empty);
        }

        [Fact]
        public void SetCPF_InvalidCPF_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.CPF = "123.456.789-00");
        }

        [Fact]
        public void SetCPF_InvalidLengthCPF_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.CPF = "123456789012");
        }

        [Fact]
        public void SetRole_ValidRole_SetsRole()
        {
            // Arrange
            var user = MockValidUser();
            var role = ERole.ADMIN;

            // Act
            user.Role = role;

            // Assert
            Assert.Equal(role, user.Role);
        }

        [Fact]
        public void SetRole_NullRole_ThrowsException()
        {
            // Arrange
            var user = MockValidUser();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => user.Role = null);
        }
    }
}