using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Stripe.Checkout;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Commands
{
    public class CreateStripeChechoutSessionCommand : IRequest<IResponseDto<Session>>
    {
        public string PriceId { get; set; }
        public string Email { get; set; }

    }
}
