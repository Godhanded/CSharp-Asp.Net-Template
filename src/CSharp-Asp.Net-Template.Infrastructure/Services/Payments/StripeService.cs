using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using PortalSession = Stripe.BillingPortal.Session;

namespace CSharp_Asp.Net_Template.Infrastructure.Services.Payments
{
    public class StripeService(IOptions<StripeOptions> stripeOptions, IStripeClient stripeClient)
    {
        private readonly StripeOptions _stripeOptions = stripeOptions.Value;
        private readonly IStripeClient _stripeClient = stripeClient;

        public async Task<Session> CreateCheckoutSession(SessionCreateOptions sessionCreateOptions)
        {
            var session = new SessionService(_stripeClient);
            return await session.CreateAsync(sessionCreateOptions);
        }

        public SessionCreateOptions GetCheckoutOptions(string customerEmail, string? customerId, string priceId, string checkoutMode = "subscription")
        {
            var sessionOptions = new SessionCreateOptions
            {
                SuccessUrl = _stripeOptions.SuccessUrl,
                AllowPromotionCodes = true,
                CancelUrl = _stripeOptions.CancelUrl,
                TaxIdCollection = new() { Enabled = true },

                LineItems = new List<SessionLineItemOptions>
                {
                    new() {
                        Price=priceId,
                        Quantity=1,
                    }
                },
                Mode = checkoutMode,
                AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true }
            };

            if (customerId is null)
                sessionOptions.CustomerEmail = customerEmail;
            else
            {
                sessionOptions.Customer = customerId;
                sessionOptions.CustomerUpdate = new() { Name = "auto" };
            }

            return sessionOptions;
        }

        public async Task<PortalSession> GetCustomerPortalSession(string customerId)
        {
            var options = new Stripe.BillingPortal.SessionCreateOptions()
            {
                Customer = customerId,
            };

            var portalService = new Stripe.BillingPortal.SessionService(_stripeClient);
            return await portalService.CreateAsync(options);
        }


        public Event ConstructEvent(string json, string stripeSignature)
        {
            return EventUtility.ConstructEvent(json, stripeSignature, _stripeOptions.WHSecrete);
        }
    }
}
