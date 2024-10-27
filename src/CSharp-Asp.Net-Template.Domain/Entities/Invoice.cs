namespace CSharp_Asp.Net_Template.Domain.Entities
{
    public class Invoice : EntityBase
    {
        public new required string Id { get; set; }
        public string? AccountCountry { get; set; }
        public string? AccountName { get; set; }
        public int AmountDue { get; set; }
        public int AmountPaid { get; set; }
        public int AmountRemaining { get; set; }
        public int AttemptCount { get; set; }
        public string? BillingReason { get; set; }
        public string? Charge { get; set; }
        public DateTime Created { get; set; }
        public string? Currency { get; set; }
        public required string CustomerId { get; set; }
        public required string CustomerEmail { get; set; }
        public string? HostedInvoiceUrl { get; set; }
        public string? InvoicePdf { get; set; }
        public string? Number { get; set; }
        public string? PaymentIntent { get; set; }
        public string? SubscriptionId { get; set; }
        public int Subtotal { get; set; }
        public int SubtotalExcludingTax { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public int Tax { get; set; }
        public required string ProductId { get; set; }
        public int Total { get; set; }
        public int TotalExcludingTax { get; set; }
        public DateTime WebhooksDeliveredAt { get; set; }
        public Guid UserId { get; set; }
        public required User User { get; set; }

    }
}
