using CSharp_Asp.Net_Template.Application.Features.Payments.Commands;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Payments;
using MediatR;
using Stripe.Checkout;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Handlers
{
    internal class CreateStripeCheckoutSessionHandler(StripeService stripeService, IAuthenticatedService authenticatedService) : IRequestHandler<CreateStripeChechoutSessionCommand, IResponseDto<Session?>>
    {
        private readonly StripeService _stripeService = stripeService;
        private readonly IAuthenticatedService _authenticatedService = authenticatedService;

        public async Task<IResponseDto<Session?>> Handle(CreateStripeChechoutSessionCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticatedService.GetCurrentUserAsync();
            if (user is null)
                return new FailureResponseDto<Session?>(null, "User Does Not Exist");

            var checkoutOptions = _stripeService.GetCheckoutOptions(user.Email, user.CustomerId, request.PriceId);
            var session = await _stripeService.CreateCheckoutSession(checkoutOptions);
            return new SuccessResponseDto<Session?>(session);
        }
    }
}
