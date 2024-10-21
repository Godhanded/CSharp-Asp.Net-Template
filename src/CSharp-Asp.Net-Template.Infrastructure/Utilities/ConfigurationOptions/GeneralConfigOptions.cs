using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions
{
    public class GeneralConfigOptions
    {
        [Required, Url]
        public required string BaseUrl { get; set; }
        [Required, MinLength(1)]
        public int TokenExpiry { get; set; }
    }
}
