using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class AuthenticatedService(IHttpContextAccessor httpCtxAccessor, IRepository<User> userRepo) : IAuthenticatedService
    {
        private readonly IHttpContextAccessor _httpCtxAccessor = httpCtxAccessor;
        private readonly IRepository<User> _userRepo = userRepo;

        public async Task<User?> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            if (userId is null)
                return null;
            return await _userRepo.GetAsync(userId.Value);
        }

        public Guid? GetCurrentUserId()
        {
            var httpContext = _httpCtxAccessor.HttpContext;
            if (httpContext?.User is null)
                return null;

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userId, out Guid result))
                return result;
            return null;
        }
    }
}
