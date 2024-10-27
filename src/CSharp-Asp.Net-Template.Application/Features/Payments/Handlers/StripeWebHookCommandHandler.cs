using AutoMapper;
using CSharp_Asp.Net_Template.Application.Features.Payments.Commands;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Payments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Stripe.Checkout;
using Invoice = CSharp_Asp.Net_Template.Domain.Entities.Invoice;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Handlers
{
    internal class StripeWebHookCommandHandler(IRepository<User> userRepository, IRepository<Invoice> invoiceRepository, StripeService stripeService, IHttpContextAccessor httpContextAccessor, ILogger<StripeWebHookCommandHandler> logger, IMapper mapper) : IRequestHandler<StripeWebHookCommand>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<Invoice> _invoiceRepository = invoiceRepository;
        private readonly StripeService _stripeService = stripeService;
        private readonly ILogger<StripeWebHookCommandHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

        public async Task Handle(StripeWebHookCommand request, CancellationToken cancellationToken)
        {
            var json = await new StreamReader(_httpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = _stripeService.ConstructEvent(json, _httpContext.Request.Headers["Stripe-Signature"]!);

            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":
                    // Payment is successful and the subscription is created.
                    // You should provision the subscription and save the customer ID to your database.
                    await HandleCheckoutCompleted(stripeEvent);
                    break;
                case "invoice.paid":
                    // Continue to provision the subscription as payments continue to be made.
                    // Store the status in your database and check when a user accesses your service.
                    // This approach helps you avoid hitting rate limits.

                    await HandleInvoicePaid(stripeEvent);
                    //send mail
                    break;
                case "invoice.payment_failed":
                    // The payment failed or the customer does not have a valid payment method.
                    // The subscription becomes past_due. Notify your customer and send them to the
                    // customer portal to update their payment information.
                    break;
                case "customer.subscription.deleted":
                    // Sent when a customer’s subscription ends.
                    // The subscription becomes cancelled. Notify your customer and
                    // revoke access

                    // send mail
                    break;
                default:
                    break;
            };
        }

        private async Task HandleCheckoutCompleted(Stripe.Event stripeEvent)
        {
            var session = stripeEvent.Data.Object as Session;
            if (session?.CustomerEmail == null || session.CustomerId == null) return;
            var user = await _userRepository.GetBySpecAsync(u => u.Email == session.CustomerEmail);

            if (user is null) return;

            user.CustomerId = session.CustomerId;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        private async Task HandleInvoicePaid(Stripe.Event stripeEvent)
        {
            var invoice = stripeEvent.Data.Object as Stripe.Invoice;
            if (invoice?.CustomerEmail == null || invoice.CustomerId == null) return;

            var oldCustomer = await _userRepository.GetBySpecAsync(u => u.Email == invoice.CustomerEmail);
            if (oldCustomer is null) return;


            oldCustomer.CurrentPeriodEnd = invoice.Lines.First().Period.End;
            oldCustomer.UpdatedAt = DateTime.UtcNow;
            oldCustomer.CustomerId = invoice.CustomerId;
            var inv = _mapper.Map<Invoice>(invoice);
            inv.User = oldCustomer;
            inv.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(oldCustomer);
            await _invoiceRepository.AddAsync(inv);
            await _invoiceRepository.SaveChangesAsync();
        }
    }
}
