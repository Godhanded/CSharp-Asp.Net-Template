using CSharp_Asp.Net_Template.Application.Features.UserManagement.Commands;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Dtos;
using CSharp_Asp.Net_Template.Application.Features.UserManagement.Queries;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharp_Asp.Net_Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Creates A New User Account
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Created User Deatils</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(SuccessResponseDto<UserLoginResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ModelStateErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IResponseDto<UserLoginResponseDto>>> RegisterUser(UserRegisterDto registerDto)
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
        [ProducesResponseType(typeof(SuccessResponseDto<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IResponseDto<UserDto>>> GetLoggedInUserDetails()
        {
            var response = await _mediator.Send(new GetLoggedInUserDetailQuery());
            return StatusCode(response.StatusCode, response);
        }
    }
}
