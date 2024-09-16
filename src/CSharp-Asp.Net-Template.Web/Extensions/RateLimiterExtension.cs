using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace CSharp_Asp.Net_Template.Web.Extensions
{
    public static class RateLimiterExtension
    {
        public static IServiceCollection AddRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(rlOpt =>
                {
                    rlOpt.AddConcurrencyLimiter("IpConcurrencyLimit", opt =>
                    {
                        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        opt.PermitLimit = 1;
                        opt.QueueLimit = 0;
                    });
                    rlOpt.AddFixedWindowLimiter("IpWindowLimit", opt =>
                    {
                        opt.AutoReplenishment = true;
                        opt.PermitLimit = 1;
                        opt.Window = TimeSpan.FromSeconds(1);
                        opt.QueueLimit = 0;
                        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    });

                    rlOpt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                    rlOpt.OnRejected = async (ctx, _) =>
                    {
                        ctx.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        ctx.HttpContext.Response.ContentType = "application/json";

                        await ctx.HttpContext.Response.WriteAsJsonAsync(new
                        {
                            success = false,
                            message = "Rate limit exceeded. Try again later.",
                            statusCode = ctx.HttpContext.Response.StatusCode,
                        });
                    };

                    //rlOpt.AddPolicy("Customlimit", ctx =>
                    //    {
                    //        var customValue = ctx.Connection.RemoteIpAddress?.ToString() ?? "UnknownIp";
                    //        return RateLimitPartition.GetConcurrencyLimiter(customValue, _ => new()
                    //        {
                    //            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    //            PermitLimit = 3,
                    //            QueueLimit = 2
                    //        });
                    //    });
                });
            return services;
        }
    }
}
