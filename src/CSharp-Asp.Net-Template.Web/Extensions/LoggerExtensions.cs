using Serilog;

namespace CSharp_Asp.Net_Template.Web.Extensions
{
    public static class LoggerExtensions
    {
        public static IHostBuilder UseSerilogConfiguredLogger(this IHostBuilder builder)
        {

            builder.UseSerilog((hostBuilderCtx, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(hostBuilderCtx.Configuration);
            });
            return builder;
        }
    }
}
