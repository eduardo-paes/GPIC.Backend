using System.Security.Cryptography;
using System.Text;
using Domain.Interfaces.Services;
using DZen.Security.Cryptography;

namespace Services
{
    public class HashService : IHashService
    {
        public string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            byte[] hashBytes = HashPasswordWithSHA3(passwordBytes, salt);
            string saltString = Convert.ToBase64String(salt);
            string hashString = Convert.ToBase64String(hashBytes);
            return $"{saltString}.{hashString}";
        }

        private static byte[] HashPasswordWithSHA3(byte[] password, byte[] salt)
        {
            byte[] saltedPassword = new byte[password.Length + salt.Length];
            Buffer.BlockCopy(password, 0, saltedPassword, 0, password.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, password.Length, salt.Length);
            using SHA3256Managed hasher = new();
            return hasher.ComputeHash(saltedPassword);
        }

        public bool VerifyPassword(string password, string? hashedPassword)
        {
            // Verifica se o hash da senha é vazio
            if (string.IsNullOrEmpty(hashedPassword))
            {
                return false;
            }

            // Verifica se a senha é vazia
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Verifica o formato do hash da senha
            string[] parts = hashedPassword.Split('.');
            if (parts.Length != 2)
            {
                return false;
            }

            // Verifica se o hash da senha é válido
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hashBytes = Convert.FromBase64String(parts[1]);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPasswordBytes = HashPasswordWithSHA3(passwordBytes, salt);
            return hashedPasswordBytes.SequenceEqual(hashBytes);
        }
    }
}