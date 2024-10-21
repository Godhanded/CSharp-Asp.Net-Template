using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands
{
    public class ResetPasswordRequestCommand : IRequest<IResponseDto<object>>
    {
        [EmailAddress, Required]
        public required string Email { get; set; }
    }
}
