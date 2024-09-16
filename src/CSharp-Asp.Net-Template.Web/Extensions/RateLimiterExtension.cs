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
                        opt.PermitLimit = 3;
                        opt.QueueLimit = 2;
                    });
                    rlOpt.AddFixedWindowLimiter("IpWindowLimit", opt =>
                    {
                        opt.AutoReplenishment = true;
                        opt.PermitLimit = 6;
                        opt.Window = TimeSpan.FromSeconds(1);
                        opt.QueueLimit = 2;
                        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    });
                    rlOpt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

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
