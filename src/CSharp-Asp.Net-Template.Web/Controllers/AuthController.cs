using CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Queries;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CSharp_Asp.Net_Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        /// <summary>
        /// Creates A New User Account
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Created User Deatils</returns>
        [HttpPost("register")]
        [EnableRateLimiting("IpWindowLimit")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SuccessResponseDto<UserLoginResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ModelStateErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IResponseDto<UserLoginResponseDto>>> RegisterUser([FromBody] UserRegisterDto registerDto)
        {
            var command = new UserRegisterCommand(registerDto);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Gets Logged in User Details,
        /// </summary>
        /// <returns>Logged In User Deatils</returns>
        [HttpGet("@me")]
        [EnableRateLimiting("IpConcurrencyLimit")]
        [ProducesResponseType(typeof(SuccessResponseDto<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponseDto<>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(FailureResponseDto<>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IResponseDto<UserDto>>> GetLoggedInUserDetails()
        {
            var response = await _mediator.Send(new GetLoggedInUserDetailQuery());
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Login User Account
        /// </summary>
        /// <param name="loginRequestDto"></param>
        [HttpPost("login")]
        [EnableRateLimiting("IpConcurrencyLimit")]
        [ProducesResponseType(typeof(SuccessResponseDto<UserLoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponseDto<>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IResponseDto<UserLoginResponseDto>>> LoginUser(UserLoginRequestDto loginRequestDto)
        {
            var command = new UserLoginCommand(loginRequestDto);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Reset User Password Request
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        [HttpPost("resetRequest")]
        [EnableRateLimiting("IpConcurrencyLimit")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SuccessResponseDto<object>), StatusCodes.Status202Accepted)]
        public async Task<ActionResult<IResponseDto<UserLoginResponseDto>>> ResetPasswordRequest(ResetPasswordRequestCommand resetPasswordRequest)
        {
            var response = await _mediator.Send(resetPasswordRequest);
            return StatusCode(response.StatusCode, response);
        }
    }
}
