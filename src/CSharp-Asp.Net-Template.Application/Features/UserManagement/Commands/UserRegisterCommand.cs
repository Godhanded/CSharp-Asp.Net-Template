using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands
{
    public class UserRegisterCommand : IRequest<IResponseDto<UserLoginResponseDto>>
    {
        public UserRegisterCommand(UserRegisterDto registerRequest)
        {
            RegisterRequest = registerRequest;
        }

        public UserRegisterDto RegisterRequest { get; }
    }
}
