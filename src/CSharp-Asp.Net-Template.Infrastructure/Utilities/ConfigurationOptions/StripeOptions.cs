using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions
{
    public class StripeOptions
    {
        [Required]
        public required string PublicKey { get; set; }
        [Required]
        public required string SecreteKey { get; set; }
        [Required]
        public required string WHSecrete { get; set; }
        [Required]
        public required string SuccessUrl { get; set; }
        public string? CancelUrl { get; set; }
    }
}
