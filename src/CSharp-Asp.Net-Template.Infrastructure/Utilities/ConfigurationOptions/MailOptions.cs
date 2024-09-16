using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions
{
    public class MailOptions
    {
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public int Port { get; set; }
    }
}
