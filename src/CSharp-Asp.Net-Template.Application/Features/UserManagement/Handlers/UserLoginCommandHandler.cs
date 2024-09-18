using AutoMapper;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Handlers
{
    public class UserLoginCommandHandler(IRepository<User> userRepository, IPasswordService passwordService, ITokenService tokenService, IMapper mapper) : IRequestHandler<UserLoginCommand, IResponseDto<UserLoginResponseDto>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;

        public async Task<IResponseDto<UserLoginResponseDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.GetBySpecAsync(u =>
                u.Email == request.UserLoginRequest.Email);

            if (userExists is null ||
                !_passwordService.IsPasswordEqual(
                    request.UserLoginRequest.Password,
                    userExists.PasswordSalt,
                    userExists.Password)) return
                    new FailureResponseDto<UserLoginResponseDto>(message: "Email or Password Incorrect", statusCode: StatusCodes.Status401Unauthorized);

            var accessToken = _tokenService.GenerateJwt(userExists);
            var userDto = _mapper.Map<UserDto>(userExists);

            return new SuccessResponseDto<UserLoginResponseDto>(new() { User = userDto, AccessToken = accessToken });
        }
    }
}
