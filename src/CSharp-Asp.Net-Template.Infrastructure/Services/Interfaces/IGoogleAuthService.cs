using Google.Apis.Auth;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken);
    }
}
