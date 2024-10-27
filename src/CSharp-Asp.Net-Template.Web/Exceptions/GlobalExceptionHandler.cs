using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CSharp_Asp.Net_Template.Web.Exceptions
{
    public class GlobalExceptionHandler(IHostEnvironment env):IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var responseObject = new FailureResponseDto<ProblemDetails>();
            var problemDetails= new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;
            if(exception is BaseException e)
            {
                httpContext.Response.StatusCode = e.StatusCode;
                problemDetails.Title = e.Message;
            }
            else
            {
                problemDetails.Title = "Something went wrong, its not you its us,Try again Later";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            //_logger.LogError(exception, "Type: {type}, Exception Message: {Message}, StackTrace: {StackTrace}",exception.GetType(), exception.Message, exception.StackTrace);

            if (env.IsDevelopment())
            {
                problemDetails.Title = exception.Message;
                problemDetails.Detail = exception.StackTrace;
            }
            
            responseObject.StatusCode=httpContext.Response.StatusCode;
            responseObject.Data = problemDetails;

            await httpContext.Response.WriteAsJsonAsync(responseObject,cancellationToken)
                .ConfigureAwait(false);

            return true;
        }
    }
}
