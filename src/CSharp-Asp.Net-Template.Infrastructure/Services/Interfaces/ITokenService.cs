using CSharp_Asp.Net_Template.Domain.Entities;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwt(User userData);
        public string GenerateJwt(User userData, int expireInMinutes);

    }
}
