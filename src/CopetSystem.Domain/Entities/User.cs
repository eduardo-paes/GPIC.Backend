using System.Net.Mail;
using System.Text.RegularExpressions;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
    public class User : Entity
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? CPF { get; private set; }
        public string? Role { get; private set; }

        public User(string name, string email, string password, string cpf, string role)
        {
            ValidateDomain(name, email, password, cpf, role);
        }

        private void ValidateDomain(string name, string email, string password, string cpf, string role)
        {
            // Name
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                ExceptionMessageFactory.Required("name"));
            DomainExceptionValidation.When(name.Length < 3,
                ExceptionMessageFactory.MinLength("name", 3));
            DomainExceptionValidation.When(name.Length > 300,
                ExceptionMessageFactory.MaxLength("name", 300));

            // Email
            DomainExceptionValidation.When(string.IsNullOrEmpty(email),
                ExceptionMessageFactory.Required("email"));
            DomainExceptionValidation.When(ValidateEmail(email),
                ExceptionMessageFactory.InvalidEmail("email"));

            // Password
            DomainExceptionValidation.When(string.IsNullOrEmpty(password),
                ExceptionMessageFactory.Required("password"));
            DomainExceptionValidation.When(password.Length < 6,
                ExceptionMessageFactory.MinLength("password", 6));

            // CPF
            DomainExceptionValidation.When(string.IsNullOrEmpty(cpf),
                ExceptionMessageFactory.Required("cpf"));

            // Extract only numbers from cpf
            cpf = GetOnlyNumbers(cpf);
            DomainExceptionValidation.When(cpf.Length < 11,
                ExceptionMessageFactory.MinLength("cpf", 11));
            DomainExceptionValidation.When(ValidateCPF(cpf),
                ExceptionMessageFactory.InvalidCpf());

            // Role
            DomainExceptionValidation.When(string.IsNullOrEmpty(role),
                ExceptionMessageFactory.Required("role"));

            Name = name;
            Email = email;
            Password = password;
            CPF = cpf;
            Role = role;
        }

        private static bool ValidateEmail(string email)
        {
            try
            {
                return new MailAddress(email) != null ? true : false;
            }
            catch
            {
                return false;
            }
        }

        private static string GetOnlyNumbers(string input) => string.Concat(input.Where(Char.IsDigit));

        private static bool ValidateCPF(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            // Check length
            if (cpf.Length != 11)
                return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum = 0;
            string tmpCPF = cpf.Substring(0, 9);

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tmpCPF[i].ToString()) * multiplier1[i];

            int rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;

            string digit = rest.ToString();
            tmpCPF = tmpCPF + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tmpCPF[i].ToString()) * multiplier2[i];

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;

            digit += rest.ToString();
            return cpf.EndsWith(digit);
        }
    }
}