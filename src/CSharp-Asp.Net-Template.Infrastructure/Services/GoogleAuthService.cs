using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using Google.Apis.Auth;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public async Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken)
        {
            return await GoogleJsonWebSignature.ValidateAsync(idToken);
        }
    }
}
