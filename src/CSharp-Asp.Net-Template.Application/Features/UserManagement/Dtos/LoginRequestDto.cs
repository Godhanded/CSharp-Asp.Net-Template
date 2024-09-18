using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
