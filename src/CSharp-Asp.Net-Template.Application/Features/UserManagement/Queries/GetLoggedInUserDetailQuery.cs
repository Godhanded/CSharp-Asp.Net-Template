using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Queries
{
    public class GetLoggedInUserDetailQuery : IRequest<IResponseDto<UserDto>>
    {
    }
}
