using CSharp_Asp.Net_Template.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwt(User userData);
        public string GenerateJwt(User userData, int expireInMinutes);

        public (string token, string tokenHash) GenerateRandomToken();

        public string ComputeSha256Hash(string rawToken);

    }
}
