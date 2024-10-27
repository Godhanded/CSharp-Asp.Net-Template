using CSharp_Asp.Net_Template.Application.Features.Payments.Commands;
using CSharp_Asp.Net_Template.Infrastructure.Services.Payments;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Stripe.Checkout;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Domain.Entities;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Handlers
{
    internal class CreateStripeCheckoutSessionHandler(StripeService stripeService, IRepository<User> userRepository) : IRequestHandler<CreateStripeChechoutSessionCommand, IResponseDto<Session?>>
    {
        private readonly StripeService _stripeService = stripeService;
        private readonly IRepository<User> _userRepository = userRepository;

        public async Task<IResponseDto<Session?>> Handle(CreateStripeChechoutSessionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);
            if (user is null)
                return new FailureResponseDto<Session?>(null, "User Deas Not Exist");

            var checkoutOptions = _stripeService.GetCheckoutOptions(user.Email, user.CustomerId, request.PriceId);
            var session = await _stripeService.CreateCheckoutSession(checkoutOptions);
            return new SuccessResponseDto<Session?>(session);
        }
    }
}
