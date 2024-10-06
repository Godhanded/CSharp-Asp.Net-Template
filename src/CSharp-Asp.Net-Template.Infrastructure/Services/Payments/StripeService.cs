using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Payments
{
    internal class StripeService(IOptions<StripeOptions> stripeOptions, IStripeClient stripeClient)
    {
        private readonly IOptions<StripeOptions> _stripeOptions = stripeOptions;
        private readonly IStripeClient _stripeClient = stripeClient;

        public async Task<Session> CreateCheckoutSession(SessionCreateOptions sessionCreateOptions)
        {
            var session = new SessionService(_stripeClient);
            // TODO: Handle Stripe Exception
            return await session.CreateAsync(sessionCreateOptions);
        }

        public SessionCreateOptions GetCheckoutOptions(string CustomerEmail, string priceId, string checkoutMode = "Subscription")
        {
            return new SessionCreateOptions
            {
                SuccessUrl = _stripeOptions.Value.SuccessUrl,
                AllowPromotionCodes = true,
                CancelUrl = _stripeOptions.Value.CancelUrl,
                CustomerEmail = CustomerEmail,

                LineItems = new List<SessionLineItemOptions>
                {
                    new() {
                        Price=priceId,
                        Quantity=1,
                    }
                },
                Mode = checkoutMode
            };
        }
    }
}
