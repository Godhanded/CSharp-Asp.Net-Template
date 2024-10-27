using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CSharp_Asp.Net_Template.Web.Exceptions
{
    public class StripeExceptionHandler(IHostEnvironment env)
        : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not StripeException)
                return false;

            var stripeException = exception as StripeException;

            var responseObject = new FailureResponseDto<ProblemDetails>();
            var problemDetails = new ProblemDetails();

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            problemDetails.Instance = httpContext.Request.Path;
            problemDetails.Title = "Something went wrong in the payment process. ";

            if (env.IsDevelopment())
            {
                problemDetails.Title = stripeException!.Message;
                problemDetails.Status = (int)stripeException.HttpStatusCode;
                problemDetails.Detail = stripeException.StackTrace;
            }

            responseObject.StatusCode = httpContext.Response.StatusCode;
            responseObject.Message = "Stripe Payment Error";
            responseObject.Data = problemDetails;

            //_logger.LogError
            //    (stripeException
            //    , "Exception Message: {Message}, StackTrace: {StackTrace}, HttpStatus Code: {}"
            //    ,stripeException.Message
            //    ,stripeException.StackTrace
            //    ,(int)stripeException.HttpStatusCode);

            await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken)
                .ConfigureAwait(false);
            return true;


        }
    }
}
