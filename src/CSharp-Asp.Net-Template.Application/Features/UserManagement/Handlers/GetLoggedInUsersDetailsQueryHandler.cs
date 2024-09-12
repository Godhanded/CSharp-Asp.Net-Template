using AutoMapper;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Queries;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using MediatR;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Handlers
{
    public class GetLoggedInUsersDetailsQueryHandler(IAuthenticatedService authenticatedService, IMapper mapper) : IRequestHandler<GetLoggedInUserDetailQuery, IResponseDto<UserDto>>
    {
        private readonly IAuthenticatedService _authenticatedService = authenticatedService;
        private readonly IMapper _mapper = mapper;

        public async Task<IResponseDto<UserDto>> Handle(GetLoggedInUserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _authenticatedService.GetCurrentUserAsync();
            if (user is null)
                return new FailureResponseDto<UserDto>(message: "Unable To Retrieve User Details");
            var userDto = _mapper.Map<UserDto>(user);

            return new SuccessResponseDto<UserDto>(userDto);
        }
    }
}
