using CSharp_Asp.Net_Template.Domain.Entities;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces
{
    public interface IAuthenticatedService
    {
        Guid? GetCurrentUserId();

        Task<User?> GetCurrentUserAsync();
    }
}
