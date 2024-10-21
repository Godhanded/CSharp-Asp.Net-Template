using CSharp_Asp.Net_Template.Application.Features.Payments.Commands;
using CSharp_Asp.Net_Template.Infrastructure.Services.Payments;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Stripe.Checkout;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Handlers
{
    internal class CreateStripeCheckoutSessionHandler(StripeService stripeService) : IRequestHandler<CreateStripeChechoutSessionCommand, IResponseDto<Session>>
    {
        private readonly StripeService _stripeService = stripeService;

        public async Task<IResponseDto<Session>> Handle(CreateStripeChechoutSessionCommand request, CancellationToken cancellationToken)
        {
            var checkoutOptions = _stripeService.GetCheckoutOptions(request.Email, request.PriceId);
            var session = await _stripeService.CreateCheckoutSession(checkoutOptions);

            return new SuccessResponseDto<Session>(session);
        }
    }
}
