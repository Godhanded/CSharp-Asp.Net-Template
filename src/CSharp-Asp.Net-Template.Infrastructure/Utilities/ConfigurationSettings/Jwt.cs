namespace CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationSettings
{
    public class Jwt
    {
        public string SecretKey { get; set; }

        public int ExpireInMinutes { get; set; }
    }
}
