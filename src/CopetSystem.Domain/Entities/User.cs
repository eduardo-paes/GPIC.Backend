using System.Data;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
    public class User : Entity
    {
        private string? _name;
        public string? Name
        {
            get { return _name; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("name"));
                DomainExceptionValidation.When(value.Length < 3,
                    ExceptionMessageFactory.MinLength("name", 3));
                DomainExceptionValidation.When(value.Length > 300,
                    ExceptionMessageFactory.MaxLength("name", 300));
                _name = value;
            }
        }

        private string? _email;
        public string? Email
        {
            get { return _email; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("email"));
                DomainExceptionValidation.When(value.Length > 300,
                    ExceptionMessageFactory.MaxLength("email", 300));
                DomainExceptionValidation.When(ValidateEmail(value),
                    ExceptionMessageFactory.InvalidEmail("email"));
                _email = value;
            }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("password"));
                DomainExceptionValidation.When(value.Length < 6,
                    ExceptionMessageFactory.MinLength("password", 6));
                DomainExceptionValidation.When(value.Length > 300,
                    ExceptionMessageFactory.MaxLength("password", 300));
                _password = value;
            }
        }

        private string? _cpf;
        public string? CPF
        {
            get { return _cpf; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("cpf"));

                // Extract only numbers from cpf
                value = GetOnlyNumbers(value);
                DomainExceptionValidation.When(value.Length != 11,
                    ExceptionMessageFactory.WithLength("cpf", 11));
                DomainExceptionValidation.When(ValidateCPF(value),
                    ExceptionMessageFactory.InvalidCpf());
                _cpf = value;
            }
        }

        private string? _role;
        public string? Role
        {
            get { return _role; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("role"));
                DomainExceptionValidation.When(value.Length > 30,
                    ExceptionMessageFactory.MaxLength("role", 30));
                _role = value;
            }
        }

        public User(string name, string email, string password, string cpf, string role)
        {
            Name = name;
            Email = email;
            Password = password;
            CPF = cpf;
            Role = role;
        }

        #region Public Setters
        public void UpdateName(string name) => Name = name;
        public void UpdatePassword(string password) => Password = password;
        public void UpdateRole(string role) => Role = role;
        #endregion

        #region Utils
        private static bool ValidateEmail(string email)
        {
            try
            {
                return new MailAddress(email) != null ? false : true;
            }
            catch
            {
                return true;
            }
        }

        private static bool ValidateCPF(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            // Check length
            if (cpf.Length != 11)
                return false;

            // Sum first digit
            int sum = 0;
            for (int i = 1; i < 10; i++)
            {
                sum += int.Parse(cpf[i-1].ToString()) * (11 - i);
            }

            // Get first digit
            int dig1 = 11 - (sum % 11);
            if (dig1 > 9) dig1 = 0;

            // Sum second digit
            sum = 0;
            for (int i = 1; i < 11; i++)
            {
                sum += int.Parse(cpf[i - 1].ToString()) * (12 - i);
            }

            // Get second digit
            int dig2 = 11 - (sum % 11);
            if (dig2 > 9) dig2 = 0;

            // Check if CPF ends with correct digits
            string digit = dig1.ToString() + dig2.ToString();
            return !cpf.EndsWith(digit);
        }

        private static string GetOnlyNumbers(string input) => string.Concat(input.Where(Char.IsDigit));
        #endregion
    }
}