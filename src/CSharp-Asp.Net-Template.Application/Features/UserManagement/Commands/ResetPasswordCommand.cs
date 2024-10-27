using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands
{
    public class ResetPasswordCommand : IRequest<IResponseDto<object?>>
    {
        [Required]
        public required string Token { get; set; }
        [Required, RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must contain at least one letter and one number")]
        public required string Password { get; set; }
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}
