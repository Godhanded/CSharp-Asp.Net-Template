using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationSettings;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class TokenService(Jwt jwtConfig) : ITokenService
    {
        private readonly Jwt _jwtConfig = jwtConfig;

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

        private static SymmetricSecurityKey GetSecurityKey(string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            return securityKey;
        }
    }
}
