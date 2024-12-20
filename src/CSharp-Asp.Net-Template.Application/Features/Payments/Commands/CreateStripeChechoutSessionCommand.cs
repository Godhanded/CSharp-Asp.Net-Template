﻿using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Stripe.Checkout;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Commands
{
    public class CreateStripeChechoutSessionCommand : IRequest<IResponseDto<Session?>>
    {
        public required string PriceId { get; set; }

    }
}
