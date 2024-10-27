using System.Text.Json.Serialization;

namespace CSharp_Asp.Net_Template.Application.Features.Payments.Dtos
{
    public class InvoiceDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("account_country")]
        public string AccountCountry { get; set; }
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; }
        [JsonPropertyName("amount_due")]
        public int AmountDue { get; set; }
        [JsonPropertyName("amount_paid")]
        public int AmountPaid { get; set; }
        [JsonPropertyName("amount_remaining")]
        public int AmountRemaining { get; set; }
        [JsonPropertyName("attempt_count")]
        public int AttemptCount { get; set; }
        [JsonPropertyName("billing_reason")]
        public string BillingReason { get; set; }
        [JsonPropertyName("charge")]
        public string Charge { get; set; }
        [JsonPropertyName("created")]
        public int Created { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("customer")]
        public required string CustomerId { get; set; }
        [JsonPropertyName("customer_email")]
        public required string CustomerEmail { get; set; }
        [JsonPropertyName("hosted_invoice_url")]
        public string? HostedInvoiceUrl { get; set; }
        [JsonPropertyName("invoice_pdf")]
        public string? InvoicePdf { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("payment_intent")]
        public string PaymentIntent { get; set; }
        [JsonPropertyName("subscription")]
        public string SubscriptionId { get; set; }
        [JsonPropertyName("subtotal")]
        public int Subtotal { get; set; }
        [JsonPropertyName("subtotal_excluding_tax")]
        public int SubtotalExcludingTax { get; set; }
        [JsonPropertyName("tax")]
        public int Tax { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("total_excluding_tax")]
        public int TotalExcludingTax { get; set; }
        [JsonPropertyName("webhooks_delivered_at")]
        public int WebhooksDeliveredAt { get; set; }
    }
}
