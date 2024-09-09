using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {

        private const int SALTSIZE = 20;

        public (string passwordSalt, string passwordHash) GeneratePasswordSaltAndHash(string plainPassword)
        {
            var buffer = RandomNumberGenerator.GetBytes(SALTSIZE);
            var salt = Convert.ToBase64String(buffer);
            string passwordHash = GeneratePasswordHash(plainPassword, salt);
            return (salt, passwordHash);
        }


        public bool IsPasswordEqual(string plainPassword, string passwordSalt, string passwordHash)
        {
            return GeneratePasswordHash(plainPassword, passwordSalt) == passwordHash;
        }

        private string GeneratePasswordHash(string plainPassword, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            var byteHash = SHA256.HashData(bytes);

            return Convert.ToBase64String(byteHash);
        }
    }
}
