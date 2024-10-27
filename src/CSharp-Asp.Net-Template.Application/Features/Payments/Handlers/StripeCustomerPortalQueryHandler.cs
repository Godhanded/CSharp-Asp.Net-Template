using CSharp_Asp.Net_Template.Application.Features.Payments.Queries;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Payments;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Handlers
{
    public class StripeCustomerPortalQueryHandler(IRepository<User> userRepository, IAuthenticatedService authenticatedService, StripeService stripeService)
        : IRequestHandler<StripeCustomerPortalQuery, IResponseDto<string?>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IAuthenticatedService _authenticatedService = authenticatedService;
        private readonly StripeService _stripeService = stripeService;

        public async Task<IResponseDto<string?>> Handle(StripeCustomerPortalQuery request, CancellationToken cancellationToken)
        {
            var user = await _authenticatedService.GetCurrentUserAsync();
            if (user is null)
                return new FailureResponseDto<string?>(null, "User not found");
            if (user.CustomerId is null)
                return new FailureResponseDto<string?>(null, "You have not purchased any billing plan", StatusCodes.Status400BadRequest);

            var session = await _stripeService.GetCustomerPortalSession(user.CustomerId);

            return new SuccessResponseDto<string?>(session.Url);
        }
    }
}
