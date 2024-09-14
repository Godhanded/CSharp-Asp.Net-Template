using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions
{
    public class JwtOptions
    {
        [Required(AllowEmptyStrings = false)]
        public string SecretKey { get; set; }
        [Range(5, 500)]
        public int ExpireInMinutes { get; set; }
    }
}
