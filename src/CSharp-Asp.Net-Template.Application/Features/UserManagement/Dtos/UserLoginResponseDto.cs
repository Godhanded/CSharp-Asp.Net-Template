using System.Text.Json.Serialization;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos
{
    public class UserLoginResponseDto
    {
        public UserDto User { get; set; }
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
