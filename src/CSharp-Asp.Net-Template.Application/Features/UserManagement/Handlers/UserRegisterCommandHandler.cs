using AutoMapper;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Domain.Enums;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.MailModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Handlers
{
    public class UserRegisterCommandHandler(IRepository<User> userRepository, IPasswordService passwordService, ITokenService tokenService, IEmailService emailService, IMapper mapper) : IRequestHandler<UserRegisterCommand, IResponseDto<UserLoginResponseDto>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailService _emailService = emailService;
        private readonly IMapper _mapper = mapper;

        public async Task<IResponseDto<UserLoginResponseDto>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository
                .GetBySpecAsync(u => u.Email == request.RegisterRequest.Email);
            if (userExists is not null)
                return new FailureResponseDto<UserLoginResponseDto>(
                    message: "This User Already Exists",
                    statusCode: StatusCodes.Status403Forbidden);

            var user = _mapper.Map<User>(request.RegisterRequest);
            (user.PasswordSalt, user.Password) = _passwordService
                .GeneratePasswordSaltAndHash(request.RegisterRequest.Password);


            await _userRepository.AddAsync(user);
            var accessToken = _tokenService.GenerateJwt(user);

            await _userRepository.SaveChangesAsync();

            var mailRequest = new MailRequest(user.Email, "Welcome", nameof(MailTemplates.WelcomeEmail));
            var welcomeModel = new WelcomeMailModel(user.Email, user.FirstName, user.CreatedAt);

            _emailService.SendEmailAsync(mailRequest, welcomeModel);

            var newUser = _mapper.Map<UserDto>(user);

            return new SuccessResponseDto<UserLoginResponseDto>(
                data: new() { AccessToken = accessToken, User = newUser },
                statusCode: StatusCodes.Status201Created);
        }
    }
}
