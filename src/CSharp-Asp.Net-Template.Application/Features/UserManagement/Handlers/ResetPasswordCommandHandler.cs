using CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CSharp_Asp.Net_Template.Application.Features.UserManagement.Handlers
{
    public class ResetPasswordCommandHandler(IRepository<UserToken> userTokenRepository, IPasswordService passwordService, ITokenService tokenService, IEmailService emailService) : IRequestHandler<ResetPasswordCommand, IResponseDto<object?>>
    {
        private readonly IRepository<UserToken> _userTokenRepository = userTokenRepository;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailService _emailService = emailService;

        public async Task<IResponseDto<object>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var hashedToken = _tokenService.ComputeSha256Hash(request.Token);
            var userToken = await _userTokenRepository
                .GetBySpecAsync(
                    ut => ut.Token == hashedToken
                    && ut.ExpirationDate >= DateTime.UtcNow
                    && ut.Used == false,
                    ut => ut.User
                    );

            if (userToken is null)
                return new FailureResponseDto<object>(null, "This Token Is Invalid Or Has Expired", StatusCodes.Status400BadRequest);

            (userToken.User.PasswordSalt, userToken.User.Password) = _passwordService
                                                        .GeneratePasswordSaltAndHash(request.Password);
            userToken.Used = true; userToken.UpdatedAt = DateTime.UtcNow;
            await _userTokenRepository.UpdateAsync(userToken);
            await _userTokenRepository.SaveChangesAsync();

            return new SuccessResponseDto<object>(null, "Password Reset Successfully");

        }
    }
}
