using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class TokenService(IOptions<JwtOptions> jwtConfig) : ITokenService
    {
        private readonly JwtOptions _jwtConfig = jwtConfig.Value;

        public static TokenValidationParameters GetTokenValidationParameters(string secretKey) => new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetSecurityKey(secretKey),
        };

        public string GenerateJwt(User userData)
        {
            return GenerateJwt(userData, _jwtConfig.ExpireInMinutes);
        }

        public string GenerateJwt(User userData, int expireInMinutes)
        {
            SymmetricSecurityKey securityKey = GetSecurityKey(_jwtConfig.SecretKey);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub, userData.Id.ToString()),
                new(JwtRegisteredClaimNames.Name, userData.FirstName)
                ];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireInMinutes),
                SigningCredentials = signingCredentials
            };

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }

        public (string token, string tokenHash) GenerateRandomToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(count: 64));
            var tokenHash = ComputeSha256Hash(token);
            return (token, tokenHash);
        }

        public string ComputeSha256Hash(string rawToken)
        {
            var digest = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));

            var builder = new StringBuilder();
            for (int i = 0; i < digest.Length; i++)
            {
                builder.Append(digest[i].ToString("x2"));
            }
            return builder.ToString();
        }

        private static SymmetricSecurityKey GetSecurityKey(string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            return securityKey;
        }
    }
}
