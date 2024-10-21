using CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Domain.Enums;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.MailModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Handlers
{
    public class ResetPasswordRequestCommandHandler(IRepository<User> userRepository, ITokenService tokenService, IEmailService emailService, IOptions<GeneralConfigOptions> generalConfigs) : IRequestHandler<ResetPasswordRequestCommand, IResponseDto<object>>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailService _emailService = emailService;
        private readonly GeneralConfigOptions _generalConfigs = generalConfigs.Value;

        public async Task<IResponseDto<object>> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBySpecAsync(u => u.Email == request.Email);
            if (user is null)
                return new SuccessResponseDto<object>(null, "A password reset link will be sent to you, if this user exists", StatusCodes.Status202Accepted);

            var (resetToken, resetTokenHash) = _tokenService.GenerateRandomToken();
            var userToken = new UserToken
            {
                Token = resetTokenHash,
                User = user,
                ExpirationDate = DateTime.UtcNow.AddHours(_generalConfigs.TokenExpiry)
            };
            user.UserTokens.Add(userToken);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            var resetUrl = $"{_generalConfigs.BaseUrl}/auth/resetPassword?token={resetToken}";

            var mailModel = new ResetPasswordRequestModel(resetUrl, _generalConfigs.TokenExpiry);
            var mailRequest = new MailRequest(user.Email, "Password Reset Request", nameof(MailTemplates.ResetPasswordRequestEmail));
            _emailService.SendEmailAsync(mailRequest, mailModel);

            return new SuccessResponseDto<object>(null, "A password reset link will be sent to you, if this user exists", StatusCodes.Status202Accepted);
        }
    }
}
