using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos
{
    public class UserLoginRequestDto
    {
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
