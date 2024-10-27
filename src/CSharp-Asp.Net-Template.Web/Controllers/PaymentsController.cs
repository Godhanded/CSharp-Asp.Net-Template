using CSharp_Asp.Net_Template.Application.Features.Payments.Commands;
using CSharp_Asp.Net_Template.Application.Features.Payments.Queries;
using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Application.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace CSharp_Asp.Net_Template.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController(ISender _mediatr) : ControllerBase
    {
        private readonly ISender _mediatr = _mediatr;

        [HttpPost("stripe/checkout")]
        [ProducesResponseType(typeof(IResponseDto<Session?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseDto<Session?>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IResponseDto<Session?>>> CreateStripeCheckoutSession(CreateStripeChechoutSessionCommand chechoutSessionCommand)
        {
            var response = await _mediatr.Send(chechoutSessionCommand);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("stripe/webhook")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> HandleSripeEvent()
        {
            var command = new StripeWebHookCommand();
            await _mediatr.Send(command);
            return Ok();
        }

        [HttpGet("stripe/portal")]
        [ProducesResponseType(typeof(SuccessResponseDto<string?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponseDto<string?>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(FailureResponseDto<string?>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IResponseDto<string?>>> GetStripeCustomerPortalSession()
        {
            var query = new StripeCustomerPortalQuery();
            var response = await _mediatr.Send(query);
            return StatusCode(response.StatusCode, response);
        }
    }
}
