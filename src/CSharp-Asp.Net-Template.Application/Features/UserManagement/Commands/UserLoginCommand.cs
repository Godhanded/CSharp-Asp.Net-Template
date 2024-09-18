using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands
{
    public class UserLoginCommand : IRequest<IResponseDto<UserLoginResponseDto>>
    {
        public UserLoginCommand(UserLoginRequestDto userLoginRequest)
        {
            UserLoginRequest = userLoginRequest;
        }

        public UserLoginRequestDto UserLoginRequest { get; }
    }
}
